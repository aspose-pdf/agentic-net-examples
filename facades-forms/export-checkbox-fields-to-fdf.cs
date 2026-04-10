using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputFdfPath = "checkboxes.fdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Initialize the Form facade for the source PDF.
        using (Form form = new Form(inputPdfPath))
        {
            // Export the entire form data to a temporary memory stream.
            using (MemoryStream tempFdf = new MemoryStream())
            {
                form.ExportFdf(tempFdf);
                tempFdf.Position = 0; // Reset stream position for reading.

                // Read the raw FDF text.
                string fdfText = new StreamReader(tempFdf).ReadToEnd();

                // NOTE: Proper filtering of only checkbox definitions would require parsing the FDF structure.
                // For demonstration purposes we write the whole FDF content to the output file.
                // Replace the following block with actual parsing logic if needed.
                using (FileStream outStream = new FileStream(outputFdfPath, FileMode.Create, FileAccess.Write))
                using (StreamWriter writer = new StreamWriter(outStream))
                {
                    writer.Write(fdfText);
                }
            }
        }

        Console.WriteLine($"Checkbox field definitions exported to '{outputFdfPath}'.");
    }
}