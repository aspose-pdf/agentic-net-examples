using System;
using System.IO;
using Aspose.Pdf; // Aspose.Pdf namespace contains Document and related classes

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPath = "input.pdf";

        // Output PDF file path (the file that will receive the data from the memory stream)
        const string outputPath = "output_from_memory.pdf";

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPath))
            {
                // Create a memory stream to hold the PDF data
                using (MemoryStream ms = new MemoryStream())
                {
                    // Save the document into the memory stream (PDF format is implied)
                    pdfDoc.Save(ms);

                    // Reset the stream position to the beginning before reading
                    ms.Position = 0;

                    // Write the contents of the memory stream to the output file
                    // Using FileStream ensures the file is created/written correctly
                    using (FileStream fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                    {
                        ms.CopyTo(fileStream);
                    }
                }
            }

            Console.WriteLine($"PDF successfully saved to memory and written to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}