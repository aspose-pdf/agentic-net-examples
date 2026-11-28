using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "survey_results.pdf";
        const string outputPath = "updated_results.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPath))
        {
            // Create FormEditor and bind it to the loaded document
            using (FormEditor formEditor = new FormEditor())
            {
                formEditor.BindPdf(pdfDoc);
                // Add the new list item "Neutral" to the ListBox named "Satisfaction"
                formEditor.AddListItem("Satisfaction", "Neutral");
                // Save the updated PDF directly via FormEditor (avoids obsolete Save())
                formEditor.Save(outputPath);
            }
        }

        Console.WriteLine($"Updated PDF saved to '{outputPath}'.");
    }
}
