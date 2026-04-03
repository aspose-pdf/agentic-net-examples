using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string sessionId  = "ABC123XYZ"; // example session identifier

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create a hidden text box field to store the session identifier
            // Rectangle with zero size makes the field invisible on the page
            Aspose.Pdf.Rectangle hiddenRect = new Aspose.Pdf.Rectangle(0, 0, 0, 0);
            TextBoxField hiddenField = new TextBoxField(doc, hiddenRect)
            {
                // Set the field name (partial name) – used for backend retrieval
                PartialName = "SessionId",
                // Assign the session identifier as the field value
                Value = sessionId,
                // Make the field read‑only and non‑exportable to keep it hidden from users
                ReadOnly = true,
                Exportable = false
            };

            // Add the field to the form on the first page (page numbers are 1‑based)
            doc.Form.Add(hiddenField, 1);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with hidden session field: {outputPath}");
    }
}