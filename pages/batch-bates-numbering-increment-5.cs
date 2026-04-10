using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class BatchBatesNumbering
{
    static void Main()
    {
        // Resolve folders relative to the executable so the code works on any OS
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;
        string inputFolder = Path.Combine(baseDir, "InputPdfs");
        string outputFolder = Path.Combine(baseDir, "OutputPdfs");

        // Ensure the directories exist
        Directory.CreateDirectory(inputFolder);
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file found in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            // Load the PDF document – the using statement disposes it correctly
            using (Document doc = new Document(pdfPath))
            {
                // -----------------------------------------------------------------
                // Manual Bates numbering (increment of 5) – avoids Facades types.
                // -----------------------------------------------------------------
                int startNumber = 1;          // first Bates number
                int increment = 5;            // increase by 5 for each page
                int numberOfDigits = 5;       // 5‑digit format (e.g., 00001, 00006, …)
                string prefix = "B-";        // optional prefix

                for (int i = 0; i < doc.Pages.Count; i++)
                {
                    int currentNumber = startNumber + i * increment;
                    string formatted = prefix + currentNumber.ToString().PadLeft(numberOfDigits, '0');

                    // Create a text fragment that will be placed near the bottom centre of the page.
                    TextFragment tf = new TextFragment(formatted)
                    {
                        // Adjust appearance as needed
                        TextState = { FontSize = 12, Font = FontRepository.FindFont("Arial") },
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Bottom,
                        // Position 20 points above the bottom edge (you can tweak this value).
                        Position = new Position(0, 20)
                    };

                    // Add the fragment to the current page.
                    doc.Pages[i + 1].Paragraphs.Add(tf);
                }

                // Build the output file path preserving the original file name
                string outputPath = Path.Combine(outputFolder, Path.GetFileName(pdfPath));

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Processed: {Path.GetFileName(pdfPath)}");
        }

        Console.WriteLine("Batch Bates numbering completed.");
    }
}
