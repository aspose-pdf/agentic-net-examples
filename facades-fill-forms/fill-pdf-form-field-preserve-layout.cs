using System;
using System.IO;
using Aspose.Pdf.Facades;   // Facade API for PDF manipulation

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";          // source PDF with form fields
        const string outputPath = "filled_output.pdf";  // destination PDF

        // Verify source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF using the Form facade (creates and loads the document)
        Form form = new Form(inputPath);

        // Example: fill a text field named "Name" – adjust field name/value as needed
        form.FillField("Name", "John Doe");

        // The Form facade preserves the original layout and appearance of the PDF.
        // Save the modified document using the facade's Save method (provided save rule).
        form.Save(outputPath);

        Console.WriteLine($"Filled PDF saved to '{outputPath}'.");
    }
}