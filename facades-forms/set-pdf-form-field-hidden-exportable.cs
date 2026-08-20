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
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Initialize FormEditor on the loaded document
            using (FormEditor formEditor = new FormEditor(doc))
            {
                // Set the field appearance to Hidden.
                // Hidden makes the field invisible in the viewer but it will still be exported.
                // Do NOT set the NoExport flag; therefore the field remains exportable.
                bool success = formEditor.SetFieldAppearance(fieldName, AnnotationFlags.Hidden);
                if (!success)
                {
                    Console.Error.WriteLine($"Failed to set appearance for field '{fieldName}'.");
                }
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Field '{fieldName}' set to hidden (exportable) and saved to '{outputPdf}'.");
    }
}