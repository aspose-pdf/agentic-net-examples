using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the field named "Score" and configure it
            if (doc.Form["Score"] is NumberField scoreField)
            {
                // Allow only digits
                scoreField.AllowedChars = "0123456789";

                // Limit the maximum number of characters to 3 (covers 0‑100)
                scoreField.MaxLen = 3;
            }
            else
            {
                Console.Error.WriteLine("Field 'Score' not found or is not a NumberField.");
                return;
            }

            // Use FormEditor to enforce the character limit (redundant safety)
            using (FormEditor formEditor = new FormEditor())
            {
                formEditor.BindPdf(doc);
                // Set maximum character count to 3
                formEditor.SetFieldLimit("Score", 3);
                // Save the modified PDF
                formEditor.Save(outputPath);
            }
        }

        Console.WriteLine($"PDF saved with 'Score' field restricted to integers 0‑100 at '{outputPath}'.");
    }
}