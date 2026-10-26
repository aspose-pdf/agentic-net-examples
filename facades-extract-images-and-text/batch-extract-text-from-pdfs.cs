using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing the PDF files to process
        const string inputFolder = @"C:\PdfInput";
        // Folder where the extracted text files will be saved
        const string outputFolder = @"C:\PdfOutput";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        // Ensure the output folder exists
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            try
            {
                // Use PdfExtractor from Aspose.Pdf.Facades to extract text
                using (PdfExtractor extractor = new PdfExtractor())
                {
                    // Bind the PDF file to the extractor
                    extractor.BindPdf(pdfPath);

                    // Extract text from the whole document
                    extractor.ExtractText();

                    // Retrieve the extracted text via a MemoryStream (GetText requires a Stream argument)
                    string extractedText;
                    using (MemoryStream textStream = new MemoryStream())
                    {
                        extractor.GetText(textStream);
                        textStream.Position = 0; // rewind to beginning
                        using (StreamReader reader = new StreamReader(textStream))
                        {
                            extractedText = reader.ReadToEnd();
                        }
                    }

                    // Build the output .txt file path (same filename, .txt extension)
                    string txtFileName = Path.GetFileNameWithoutExtension(pdfPath) + ".txt";
                    string txtPath = Path.Combine(outputFolder, txtFileName);

                    // Write the extracted text to the .txt file
                    File.WriteAllText(txtPath, extractedText);
                }

                Console.WriteLine($"Extracted text from '{Path.GetFileName(pdfPath)}' to '{Path.GetFileNameWithoutExtension(pdfPath)}.txt'");
            }
            catch (Exception ex)
            {
                // Log any errors but continue processing other files
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch text extraction completed.");
    }
}
