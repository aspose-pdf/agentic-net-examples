using System;
using System.IO;
using Aspose.Pdf;

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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate pages using 1‑based indexing (Aspose.Pdf requirement)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                // OPTIONAL: set a new TrimBox to demonstrate cropping
                // Replace with your own rectangle if needed
                Aspose.Pdf.Rectangle newTrim = new Aspose.Pdf.Rectangle(50, 50, 500, 700);
                doc.Pages[i].TrimBox = newTrim;

                // Retrieve the TrimBox after any cropping operation
                Aspose.Pdf.Rectangle trim = doc.Pages[i].TrimBox;

                // Output the TrimBox coordinates for verification
                Console.WriteLine($"Page {i} TrimBox: LLX={trim.LLX}, LLY={trim.LLY}, URX={trim.URX}, URY={trim.URY}");
            }

            // If you want to persist the changes, uncomment the line below
            // doc.Save("output_cropped.pdf");
        }
    }
}