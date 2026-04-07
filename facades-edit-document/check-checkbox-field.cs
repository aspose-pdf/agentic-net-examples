using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName = "TermsAccepted";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Initialize the Form facade with the loaded document
            using (Form pdfForm = new Form(doc))
            {
                // Set the checkbox field to true
                bool filled = pdfForm.FillField(fieldName, true);
                if (!filled)
                {
                    Console.Error.WriteLine($"Field '{fieldName}' not found or could not be filled.");
                }
            }

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Checkbox '{fieldName}' set to true and saved to '{outputPath}'.");
    }
}