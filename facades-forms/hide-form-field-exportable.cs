using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string fieldName = "EmployeeID";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Initialize the FormEditor facade with the loaded document
            FormEditor formEditor = new FormEditor(doc);

            // Set the field appearance to NoView (hidden but still exportable)
            bool result = formEditor.SetFieldAppearance(fieldName, AnnotationFlags.NoView);
            if (!result)
            {
                Console.Error.WriteLine($"Failed to set appearance for field '{fieldName}'.");
            }

            // Save the modified PDF
            formEditor.Save(outputPdf);
        }

        Console.WriteLine($"Field '{fieldName}' set to hidden (exportable) and saved to '{outputPdf}'.");
    }
}