using System;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Security;
using System.Security.Cryptography.X509Certificates;

namespace CustomSecurityDemo
{
    // Custom security handler that logs password checks and encryption actions.
    public class LoggingSecurityHandler : ICustomSecurityHandler
    {
        private string _userPassword;
        private string _ownerPassword;

        // Store the last encryption parameters (optional, for demonstration).
        private EncryptionParameters _encryptionParameters;

        // ICustomSecurityHandler properties – simple placeholder values.
        public string Filter => "Standard";
        public int KeyLength => 128;
        public int Revision => 4;
        public string SubFilter => "adbe.pkcs7.s5";
        // The interface expects an integer for the PDF version. Use a sensible default (e.g., 1 for version 1.x).
        public int Version => 1;

        // Called during encryption to calculate the encryption key.
        public byte[] CalculateEncryptionKey(string userKey)
        {
            Console.WriteLine($"[LoggingSecurityHandler] CalculateEncryptionKey called with userKey='{userKey}'.");
            // Simple placeholder implementation.
            return System.Text.Encoding.UTF8.GetBytes(userKey);
        }

        // Decrypt data – placeholder that returns the original data.
        public byte[] Decrypt(byte[] data, int offset, int count, byte[] key)
        {
            Console.WriteLine("[LoggingSecurityHandler] Decrypt called.");
            // In a real implementation you would decrypt using the key.
            // Here we simply return the slice of data unchanged.
            byte[] result = new byte[count];
            Array.Copy(data, offset, result, 0, count);
            return result;
        }

        // Encrypt data – placeholder that returns the original data.
        public byte[] Encrypt(byte[] data, int offset, int count, byte[] key)
        {
            Console.WriteLine("[LoggingSecurityHandler] Encrypt called.");
            // In a real implementation you would encrypt using the key.
            // Here we simply return the slice of data unchanged.
            byte[] result = new byte[count];
            Array.Copy(data, offset, result, 0, count);
            return result;
        }

        // Encrypt the permissions integer – simple conversion to byte array.
        public byte[] EncryptPermissions(int permissions)
        {
            Console.WriteLine($"[LoggingSecurityHandler] EncryptPermissions called with permissions={permissions}.");
            return BitConverter.GetBytes(permissions);
        }

        // Generate the owner key – store passwords and log.
        public byte[] GetOwnerKey(string userPassword, string ownerPassword)
        {
            _userPassword = userPassword;
            _ownerPassword = ownerPassword;
            Console.WriteLine("[LoggingSecurityHandler] GetOwnerKey called.");
            // Placeholder: return empty array.
            return new byte[0];
        }

        // Generate the user key – store password and log.
        public byte[] GetUserKey(string userPassword)
        {
            _userPassword = userPassword;
            Console.WriteLine("[LoggingSecurityHandler] GetUserKey called.");
            // Placeholder: return empty array.
            return new byte[0];
        }

        // Initialize with encryption parameters – store for possible later use.
        public void Initialize(EncryptionParameters encryptionParameters)
        {
            _encryptionParameters = encryptionParameters;
            Console.WriteLine("[LoggingSecurityHandler] Initialize called.");
        }

        // Check if supplied password is the owner password – log and compare.
        public bool IsOwnerPassword(string password)
        {
            bool result = string.Equals(password, _ownerPassword);
            Console.WriteLine($"[LoggingSecurityHandler] IsOwnerPassword called. Result: {result}");
            return result;
        }

        // Check if supplied password is the user password – log and compare.
        public bool IsUserPassword(string password)
        {
            bool result = string.Equals(password, _userPassword);
            Console.WriteLine($"[LoggingSecurityHandler] IsUserPassword called. Result: {result}");
            return result;
        }
    }

    class Program
    {
        static void Main()
        {
            const string inputPdf = "input.pdf";
            const string encryptedPdf = "encrypted.pdf";
            const string userPassword = "user123";
            const string ownerPassword = "owner123";

            // Ensure the input file exists.
            if (!System.IO.File.Exists(inputPdf))
            {
                Console.Error.WriteLine($"Input file not found: {inputPdf}");
                return;
            }

            // Create a custom security handler instance.
            ICustomSecurityHandler customHandler = new LoggingSecurityHandler();

            // Load the document, encrypt it with the custom handler, and save.
            using (Document doc = new Document(inputPdf))
            {
                // Define permissions (example: allow printing and content extraction).
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                // Encrypt using the custom handler.
                doc.Encrypt(userPassword, ownerPassword, perms, customHandler);
                doc.Save(encryptedPdf);
                Console.WriteLine($"Document encrypted and saved to '{encryptedPdf}'.");
            }

            // Attempt to open the encrypted document with the user password.
            // The PDF library will invoke the custom handler's password checks,
            // which will be logged to the console.
            using (Document encryptedDoc = new Document(encryptedPdf, userPassword))
            {
                Console.WriteLine("Encrypted document opened successfully with user password.");
                // Perform any additional operations here if needed.
            }
        }
    }
}
