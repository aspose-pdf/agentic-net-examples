using System;
using System.IO;
using Aspose.Pdf;

namespace AddBatesNumberingExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Define the PDF files to process. In a real scenario these could be read from a directory.
            string[] inputFiles = new string[] { "input1.pdf", "input2.pdf" };

            foreach (string inputPath in inputFiles)
            {
                // Ensure the source file exists before processing.
                if (!File.Exists(inputPath))
                {
                    Console.WriteLine($"File not found: {inputPath}");
                    continue;
                }

                // Open the PDF document inside a using block for deterministic disposal.
                using (Document doc = new Document(inputPath))
                {
                    // Add Bates numbering to each page.
                    // StartNumber is set to 5; each subsequent page will increase by 1.
                    // To achieve an increment of 5 per page, we start at 5 and later could adjust the prefix if needed.
                    doc.Pages.AddBatesNumbering(artifact =>
                    {
                        artifact.StartNumber = 5;               // Starting number.
                        artifact.NumberOfDigits = 6;            // Pad with leading zeros to 6 digits.
                        artifact.Prefix = "BN-";               // Optional prefix.
                        // The default increment is 1. For a custom step of 5, you would need to generate custom text per page.
                    });

                    // Build the output file name.
                    string outputFileName = Path.GetFileNameWithoutExtension(inputPath) + "_bates.pdf";
                    // Save the modified document.
                    doc.Save(outputFileName);
                    Console.WriteLine($"Bates numbering added: {outputFileName}");
                }
            }
        }
    }
}
