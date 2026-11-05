using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Wrap Document in a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Verify that the PDF contains a form with fields
            if (doc.Form == null || doc.Form.Fields == null)
            {
                Console.WriteLine("No form fields found in the document.");
                return;
            }

            // LINQ query: retrieve only text box fields
            List<TextBoxField> textBoxFields = doc.Form.Fields
                .OfType<TextBoxField>()
                .ToList();

            Console.WriteLine($"Text box fields found: {textBoxFields.Count}");
            foreach (TextBoxField tb in textBoxFields)
            {
                // PartialName is the field's name; Value holds the entered text
                Console.WriteLine($"Name: {tb.PartialName}, Value: {tb.Value}");
            }
        }
    }
}