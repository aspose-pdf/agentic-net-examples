using System;
using System.IO;
using System.Drawing; // needed for System.Drawing.Rectangle
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string ownerPassword = "owner123"; // set to empty string if unknown

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Work on a temporary copy because PdfFileSecurity works with file paths
        string tempPath = Path.GetTempFileName();
        File.Copy(inputPath, tempPath, true);

        // Attempt to decrypt the PDF (if it is encrypted)
        using (PdfFileSecurity security = new PdfFileSecurity())
        {
            security.BindPdf(tempPath);
            // Returns true if decryption succeeded or if the file was not encrypted
            security.TryDecryptFile(ownerPassword);
        }

        // Modify the PDF content using PdfContentEditor (a Facades class)
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(tempPath);

            // System.Drawing.Rectangle expects (x, y, width, height)
            // Original coordinates: llx=100, lly=500, urx=300, ury=550
            // Width = urx - llx = 200, Height = ury - lly = 50
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 500, 200, 50);

            // Parameters: rectangle, text, font name, isBold, color name, font size
            editor.CreateText(rect, "Modified by Aspose.Pdf.Facades", "Helvetica", false, "Black", 12);

            // Save changes back to the temporary file
            editor.Save(tempPath);
        }

        // Move the processed file to the final output location
        File.Copy(tempPath, outputPath, true);
        File.Delete(tempPath);

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}
