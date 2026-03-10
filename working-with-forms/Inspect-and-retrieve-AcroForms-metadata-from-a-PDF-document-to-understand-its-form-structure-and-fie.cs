using System;
using System.IO;
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

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Retrieve the AcroForm object from the document
                Form acroForm = doc.Form;

                // Output basic form metadata
                Console.WriteLine($"Form type   : {acroForm.Type}");
                Console.WriteLine($"Has XFA     : {acroForm.HasXfa}");
                Console.WriteLine($"Field count : {acroForm.Count}");

                // Iterate through each field in the form and display its properties
                foreach (Field field in acroForm.Fields)
                {
                    Console.WriteLine("-----");
                    Console.WriteLine($"Full name       : {field.FullName}");
                    Console.WriteLine($"Partial name    : {field.PartialName}");
                    Console.WriteLine($"Alternate name  : {field.AlternateName}");
                    Console.WriteLine($"Field class     : {field.GetType().Name}");
                    Console.WriteLine($"Required        : {field.Required}");
                    Console.WriteLine($"Read‑only       : {field.ReadOnly}");
                    Console.WriteLine($"Value           : {field.Value}");
                    Console.WriteLine($"Page index (1‑based) : {field.PageIndex}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}