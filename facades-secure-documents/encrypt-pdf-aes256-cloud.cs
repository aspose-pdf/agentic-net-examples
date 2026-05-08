using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

public class PdfEncryptionUtility
{
    /// <summary>
    /// Encrypts a PDF file using AES‑256 and writes the result to the specified destination.
    /// The destination can be a path that points to a cloud storage bucket (e.g., an S3 URL or Azure Blob URL).
    /// </summary>
    /// <param name="inputPdfPath">Full path or URL of the source PDF.</param>
    /// <param name="outputPdfPath">Full path or URL of the encrypted PDF (cloud bucket location).</param>
    /// <param name="userPassword">Password required for opening the PDF (can be null or empty).</param>
    /// <param name="ownerPassword">Owner password (can be null or empty; a random one will be generated if omitted).</param>
    public void EncryptPdf(string inputPdfPath, string outputPdfPath, string userPassword, string ownerPassword)
    {
        // Ensure the input file exists before proceeding.
        if (string.IsNullOrWhiteSpace(inputPdfPath))
            throw new ArgumentException("Input PDF path must be provided.", nameof(inputPdfPath));

        // Create the PdfFileSecurity facade. It implements IDisposable, so wrap it in a using block.
        using (PdfFileSecurity fileSecurity = new PdfFileSecurity())
        {
            // Bind the source PDF file to the facade.
            fileSecurity.BindPdf(inputPdfPath);

            // Encrypt the PDF using AES‑256.
            // DocumentPrivilege.Print is used as an example; adjust privileges as needed.
            // KeySize.x256 selects a 256‑bit key, and Algorithm.AES specifies the AES algorithm.
            fileSecurity.EncryptFile(
                userPassword,
                ownerPassword,
                DocumentPrivilege.Print,
                KeySize.x256,
                Algorithm.AES);

            // Save the encrypted PDF to the destination (cloud bucket path).
            fileSecurity.Save(outputPdfPath);
        }
    }
}

public static class Program
{
    /// <summary>
    /// Simple entry point required for a console‑style build. Demonstrates usage of PdfEncryptionUtility.
    /// </summary>
    public static void Main(string[] args)
    {
        // Expected arguments: inputPdfPath outputPdfPath [userPassword] [ownerPassword]
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: <inputPdfPath> <outputPdfPath> [userPassword] [ownerPassword]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string userPwd = args.Length > 2 ? args[2] : null;
        string ownerPwd = args.Length > 3 ? args[3] : null;

        var encryptor = new PdfEncryptionUtility();
        encryptor.EncryptPdf(inputPath, outputPath, userPwd, ownerPwd);
        Console.WriteLine("PDF encryption completed successfully.");
    }
}