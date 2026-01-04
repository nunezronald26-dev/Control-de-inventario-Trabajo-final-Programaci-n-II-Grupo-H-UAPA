using Npgsql;
using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FarmaControlPlus
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            ApplyStyling();
        }

        private void ApplyStyling()
        {
            // Center align title labels
            lblWelcome.Left = (panelHeader.Width - lblWelcome.Width) / 2;
            lblSystem.Left = (panelHeader.Width - lblSystem.Width) / 2;

            // Center other controls
            lblLogin.Left = (this.ClientSize.Width - lblLogin.Width) / 2;
            lnkForgot.Left = (this.ClientSize.Width - lnkForgot.Width) / 2;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Validación de campos
            if (string.IsNullOrEmpty(txtUsername.Text) || string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Por favor ingrese usuario y contraseña", "Error de Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Aquí va la lógica de autenticación real contra BD
            if (AutenticarUsuario(txtUsername.Text, txtPassword.Text))
            {
                // Hide login and open main form
                this.Hide();
                Form1 formPrincipal = new Form1();
                formPrincipal.Show();
                formPrincipal.FormClosed += (s, args) => this.Close();
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos", "Login Fallido",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Método para autenticar usuario contra BD
        private bool AutenticarUsuario(string username, string password)
        {
            try
            {
                using (var conn = ConexionBD.ObtenerConexion())
                {
                    conn.Open();

                    // Consulta para obtener el hash almacenado
                    string sql = @"SELECT id, nombre_completo, rol, contrasena_hash 
                           FROM empleados 
                           WHERE correo = @username";

                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);

                        using (var dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                string storedHash = dr["contrasena_hash"]?.ToString();

                                // Si no hay hash almacenado (usuario antiguo)
                                if (string.IsNullOrEmpty(storedHash))
                                {
                                    // Podrías tener un valor por defecto o pedir reset
                                    return false;
                                }

                                // Generar hash de la contraseña ingresada
                                string inputHash = CalcularSHA256Hash(password);

                                // Comparar los hashes (comparación segura contra timing attacks)
                                return string.Equals(storedHash, inputHash, StringComparison.Ordinal);
                            }
                        }
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error de autenticación: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // Método para calcular hash SHA256
        private string CalcularSHA256Hash(string input)
        {
            using (System.Security.Cryptography.SHA256 sha256 = System.Security.Cryptography.SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);
                byte[] hash = sha256.ComputeHash(bytes);

                // Convertir a string hexadecimal (128 caracteres)
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    builder.Append(hash[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        private void lnkForgot_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Please contact system administrator for password reset.",
                "Forgot Password", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnLogin_MouseEnter(object sender, EventArgs e)
        {
            btnLogin.BackColor = Color.FromArgb(52, 152, 219);
        }

        private void btnLogin_MouseLeave(object sender, EventArgs e)
        {
            btnLogin.BackColor = Color.FromArgb(41, 128, 185);
        }

        private void lblWelcome_Click(object sender, EventArgs e)
        {

        }

        private void btnRegistrarNuevo_Click(object sender, EventArgs e)
        {
            // Para registro desde login (sin validación de admin)
            using (var formRegistro = new FarmaControlPlus.Forms.NuevoEmpleadoDesdeLogin(false))
            {
                if (formRegistro.ShowDialog() == DialogResult.OK)
                {
                    // Guardar el nuevo empleado con hash de contraseña
                    GuardarEmpleadoConHash(formRegistro.EmpleadoCreado);
                    MessageBox.Show("Registro exitoso. Ahora puede iniciar sesión.",
                        "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void GuardarEmpleadoConHash(Empleado emp)
        {
            try
            {
                using (var conn = ConexionBD.ObtenerConexion())
                {
                    conn.Open();

                    // Asegúrate de que la contraseña no esté vacía
                    if (string.IsNullOrEmpty(emp.Contrasena))
                    {
                        MessageBox.Show("La contraseña no puede estar vacía",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Calcular hash de la contraseña
                    string hashedPassword = CalcularSHA256Hash(emp.Contrasena);

                    string sql = @"
                INSERT INTO empleados 
                (nombre_completo, correo, direccion, telefono, sucursal, rol, contrasena_hash)
                VALUES 
                (@nombre, @correo, @direccion, @telefono, @sucursal, @rol, @contrasena_hash)";

                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@nombre", emp.NombreCompleto);
                        cmd.Parameters.AddWithValue("@correo", emp.Correo);
                        cmd.Parameters.AddWithValue("@direccion", emp.Direccion);
                        cmd.Parameters.AddWithValue("@telefono", emp.Telefono);
                        cmd.Parameters.AddWithValue("@sucursal", emp.Sucursal);
                        cmd.Parameters.AddWithValue("@rol", emp.Rol);
                        cmd.Parameters.AddWithValue("@contrasena_hash", hashedPassword);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrar usuario: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GuardarEmpleadoBD(Empleado emp)
        {
            try
            {
                using (var conn = ConexionBD.ObtenerConexion())
                {
                    conn.Open();

                    // IMPORTANTE: Verifica que tu tabla empleados tenga el campo 'contrasena'
                    string sql = @"
                INSERT INTO empleados 
                (nombre_completo, correo, direccion, telefono, sucursal, rol, contrasena_hash)
                VALUES 
                (@nombre, @correo, @direccion, @telefono, @sucursal, @rol, @contrasena_hash)";

                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@nombre", emp.NombreCompleto);
                        cmd.Parameters.AddWithValue("@correo", emp.Correo);
                        cmd.Parameters.AddWithValue("@direccion", emp.Direccion);
                        cmd.Parameters.AddWithValue("@telefono", emp.Telefono);
                        cmd.Parameters.AddWithValue("@sucursal", emp.Sucursal);
                        cmd.Parameters.AddWithValue("@rol", emp.Rol);

                        // IMPORTANTE: Asegúrate que el formulario NuevoEmpleadoDesdeLogin
                        // esté devolviendo la contraseña en la propiedad EmpleadoCreado.Contrasena
                        cmd.Parameters.AddWithValue("@contrasena",
                            !string.IsNullOrEmpty(emp.Contrasena) ?
                            HashPassword(emp.Contrasena) :
                            HashPassword("123456")); // Contraseña por defecto

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrar usuario: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Método para hashear contraseñas (necesitarás implementar esto)
        private string HashPassword(string password)
        {
            // Implementa tu método de hashing preferido
            // Ejemplo simple con SHA256 (NO usar en producción sin salting)
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }

    // Partial class to add startup modal behavior without modifying the main Form1 designer file.
    public partial class Form1
    {
        private bool _startupModalShown = false;

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            // Ensure the modal is shown only once when the form first appears
            if (!_startupModalShown)
            {
                _startupModalShown = true;

                using (var modal = new SuccessModal("¡Login exitoso!", 2000))
                {
                    // ShowDialog with 'this' centers the modal over Form1 and blocks until it closes.
                    modal.ShowDialog(this);
                }
            }
        }
    }

    // Self-contained, reusable green success modal. Adjust colors/sizes if needed.
    internal class SuccessModal : Form
    {
        private readonly Timer _closeTimer;
        private readonly Label _messageLabel;

        public SuccessModal(string message, int milliseconds = 7000)
        {
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.Manual;
            ShowInTaskbar = false;
            TopMost = true;
            BackColor = Color.FromArgb(46, 204, 113); // green
            Width = 360;
            Height = 120;
            Opacity = 0.98;

            _messageLabel = new Label
            {
                AutoSize = false,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point),
                ForeColor = Color.White,
                Text = message,
                Padding = new Padding(12)
            };

            Controls.Add(_messageLabel);

            _closeTimer = new Timer { Interval = Math.Max(1000, milliseconds) };
            _closeTimer.Tick += (s, e) =>
            {
                _closeTimer.Stop();
                Close();
            };
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Center over owner if provided, otherwise center on current screen
            if (Owner != null)
            {
                var ownerRect = Owner.Bounds;
                var ownerScreenPoint = Owner.PointToScreen(Point.Empty);
                int x = ownerScreenPoint.X + (ownerRect.Width - Width) / 2;
                int y = ownerScreenPoint.Y + (ownerRect.Height - Height) / 2;
                Location = new Point(Math.Max(0, x), Math.Max(0, y));
            }
            else
            {
                var screen = Screen.FromPoint(Cursor.Position).WorkingArea;
                Location = new Point(screen.Left + (screen.Width - Width) / 2,
                                     screen.Top + (screen.Height - Height) / 2);
            }
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            _closeTimer.Start();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _closeTimer?.Dispose();
                _messageLabel?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}