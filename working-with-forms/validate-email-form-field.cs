using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string email = "user@example.com";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Validate email contains '@' before proceeding
        if (string.IsNullOrEmpty(email) || !email.Contains("@"))
        {
            Console.Error.WriteLine("Invalid email address. PDF will not be saved.");
            return;
        }

        try
        {
            // Document lifecycle must be wrapped in a using block
            using (Document doc = new Document(inputPath))
            {
                // Example modification: add a text annotation with the email
                Page page = doc.Pages[1]; // 1‑based indexing
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
                Aspose.Pdf.Annotations.TextAnnotation annotation = new Aspose.Pdf.Annotations.TextAnnotation(page, rect)
                {
                    Title = "Contact",
                    Contents = $"Email: {email}",
                    Open = true,
                    Icon = Aspose.Pdf.Annotations.TextIcon.Note
                };
                page.Annotations.Add(annotation);

                // Save only after email validation succeeded
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}