using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_no_buttons.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document pdfDocument = new Document(inputPath);

        // Collect the names of all button fields (cannot remove while iterating the collection)
        List<string> buttonFieldNames = new List<string>();
        foreach (var field in pdfDocument.Form.Fields)
        {
            if (field is ButtonField)
            {
                buttonFieldNames.Add(field.PartialName);
            }
        }

        // Delete the identified button fields
        foreach (string name in buttonFieldNames)
        {
            pdfDocument.Form.Delete(name);
            Console.WriteLine($"Deleted button field: {name}");
        }

        // Save the modified PDF
        pdfDocument.Save(outputPath);
        Console.WriteLine($"PDF saved without button fields to '{outputPath}'.");
    }
}
