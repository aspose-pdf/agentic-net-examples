using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";          // source PDF to fill
        const string protectedPdfPath = "protected.pdf";  // final password‑protected PDF
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        try
        {
            // ---------- Fill the PDF ----------
            using (Document doc = new Document(inputPdfPath))
            {
                const string fieldName = "SampleField";
                // Ensure the form and the specific field exist
                if (doc.Form != null && doc.Form.HasField(fieldName))
                {
                    // The indexer returns a WidgetAnnotation; cast it to Field before using Value.
                    Field? field = doc.Form[fieldName] as Field;
                    if (field != null)
                    {
                        field.Value = "Filled value";
                    }
                }

                // ---------- Apply password protection ----------
                // Use Document.Encrypt for AES‑256 encryption (no extra NuGet packages required).
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                // Save the protected document directly.
                doc.Save(protectedPdfPath);
            }

            Console.WriteLine($"Password‑protected PDF saved to '{protectedPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
