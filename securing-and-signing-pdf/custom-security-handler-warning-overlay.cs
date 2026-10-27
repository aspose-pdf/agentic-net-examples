using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "secured_output.pdf";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";
        const string warningText = "Confidential – Do not copy or distribute";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Add a visible warning overlay on each page
                foreach (Page page in doc.Pages)
                {
                    // Rectangle covering the top part of the page (full width, 50 points height)
                    Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(0, page.PageInfo.Height, page.PageInfo.Width, page.PageInfo.Height - 50);

                    // Create a text annotation as the warning overlay
                    TextAnnotation warning = new TextAnnotation(page, rect)
                    {
                        Title = "Security Warning",
                        Contents = warningText,
                        Open = true,
                        Icon = TextIcon.Note,
                        // Optional: set a background color for visibility
                        Color = Aspose.Pdf.Color.Yellow
                    };

                    // Attach the annotation to the page
                    page.Annotations.Add(warning);
                }

                // Define permissions: allow printing only, thereby disabling content extraction (copy‑paste)
                Permissions perms = Permissions.PrintDocument;

                // Encrypt the document using AES‑256
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                // Save the secured PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Secured PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}