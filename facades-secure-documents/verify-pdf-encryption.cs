using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputDirectory = "pdfs";
        const string reportFile = "report.txt";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";
        const Permissions permissions = Permissions.PrintDocument | Permissions.ExtractContent;
        const CryptoAlgorithm targetAlgorithm = CryptoAlgorithm.AESx256;

        if (!Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDirectory}");
            return;
        }

        string[] pdfFiles = Directory.GetFiles(inputDirectory, "*.pdf");
        int filesToProcess = Math.Min(pdfFiles.Length, 4); // evaluation mode limit

        using (StreamWriter reportWriter = new StreamWriter(reportFile))
        {
            reportWriter.WriteLine("PDF Encryption Usage Report");
            reportWriter.WriteLine("Generated on: " + DateTime.Now);
            reportWriter.WriteLine();

            for (int i = 0; i < filesToProcess; i++)
            {
                string pdfPath = pdfFiles[i];
                string fileName = Path.GetFileName(pdfPath);
                bool isEncrypted = false;
                bool reencrypted = false;
                CryptoAlgorithm usedAlgorithm = CryptoAlgorithm.AESx256; // default placeholder

                try
                {
                    using (Document doc = new Document(pdfPath))
                    {
                        // Opened without password – assume not encrypted
                        isEncrypted = false;
                    }
                }
                catch (InvalidPasswordException)
                {
                    // Document is encrypted; we cannot read the algorithm directly.
                    // For demonstration, re‑encrypt with the desired algorithm.
                    isEncrypted = true;
                    try
                    {
                        using (Document encryptedDoc = new Document(pdfPath, userPassword))
                        {
                            encryptedDoc.Encrypt(userPassword, ownerPassword, permissions, targetAlgorithm);
                            encryptedDoc.Save(pdfPath); // overwrite with AES‑256 encryption
                            reencrypted = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine($"Failed to re‑encrypt {fileName}: {ex.Message}");
                    }
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error processing {fileName}: {ex.Message}");
                    continue;
                }

                reportWriter.WriteLine($"File: {fileName}");
                if (isEncrypted)
                {
                    reportWriter.WriteLine("  Status: Encrypted");
                    if (reencrypted)
                    {
                        reportWriter.WriteLine($"  Action: Re‑encrypted using {targetAlgorithm}");
                    }
                    else
                    {
                        reportWriter.WriteLine("  Action: Unable to re‑encrypt (check passwords)");
                    }
                }
                else
                {
                    reportWriter.WriteLine("  Status: Not encrypted");
                }
                reportWriter.WriteLine();
            }
        }

        Console.WriteLine($"Report generated: {reportFile}");
    }
}
