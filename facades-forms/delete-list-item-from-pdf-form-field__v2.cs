using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output30.pdf";
        const string fieldName = "DepartmentList";
        const string itemToDelete = "HR";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document; using ensures deterministic disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // Bind the FormEditor facade to the loaded document
            FormEditor formEditor = new FormEditor();
            formEditor.BindPdf(pdfDoc);

            // Delete the list item "HR" from the list field "DepartmentList"
            formEditor.DelListItem(fieldName, itemToDelete);

            // Save the modified PDF
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}