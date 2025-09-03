using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public sealed class LogService
    {
        private static LogService _instance;
        private static readonly object _lock = new object();
        private readonly string _logFilePath;

        private LogService()
        {
            _logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logger", "syslog.txt");
            Directory.CreateDirectory(Path.GetDirectoryName(_logFilePath) ?? string.Empty);
        }

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

        private async Task WriteLog(string message)
        {
            try
            {
                await File.AppendAllTextAsync(_logFilePath, message + Environment.NewLine);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al escribir en el archivo de log", ex);
            }
        }

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
    }
}
