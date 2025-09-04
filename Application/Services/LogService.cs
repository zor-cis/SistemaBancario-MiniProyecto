using Application.DTOs;
//Esta clase es la responsabe de manejar el servicio de logueo de todo el sistema.
namespace Application.Services
{
    public sealed class LogService
    {
        private static LogService _instance;
        private static readonly object _lock = new object();
        private readonly string _logFilePath;

        //Constructor privado para evitar la instanciacion externa y crea el archivo de log.
        private LogService()
        {
            _logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logger", "syslog.txt");
            Directory.CreateDirectory(Path.GetDirectoryName(_logFilePath) ?? string.Empty);
        }

        //Se implementa el patron singleton para asegurarse que solo exista un solo log en todo el sistema.
        public static LogService Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new LogService();
                        }
                    }
                }
                return _instance;
            }
        }
        //Este metodo escribira los mensajes en el archivo de log
        private async Task WriteLog(string message)
        {
            try
            {
                await File.AppendAllTextAsync(_logFilePath, message + Environment.NewLine); 
                // Agrega una nueva línea después de cada mensaje para que cada log este separado y sea entendible.
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al escribir en el archivo de log", ex);
            }
        }

        //El siguiente metodo registra en el log los intentos de inicio de sesion de usuario
        public async Task LogLoginUser(string email, bool Exited)
        {
            try
            {
                var result = Exited ? "exitoso" : "fallido";
                var logMessage = $"{DateTime.Now: dd-MM-yyyy} Login {result} - Email: {email}";
                await WriteLog(logMessage);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al registrar el inicio de sesión", ex);
            }
        }

        //El sieguiente metodo registra en el log los intentos de registro de usuario
        public async Task LogSignUpUser(string email, bool Exited)
        {
            try
            {
                var result = Exited ? "exitoso" : "fallido";
                var logMessage = $"{DateTime.Now: dd-MM-yyyy} Registro {result} - Email: {email}";
                await WriteLog(logMessage);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al registrar el inicio de sesión", ex);
            }
        }
    }
}
