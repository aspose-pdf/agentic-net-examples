using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Folder containing source PDFs
        string inputFolder = "InputPdfs";
        // Folder where processed PDFs will be saved
        string outputFolder = "OutputPdfs";
        // Path to the predefined graph image to insert
        string graphImagePath = "graph.png";
        // Text to display inside the logo rectangle
        string logoText = "Company Logo";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        if (!File.Exists(graphImagePath))
        {
            Console.Error.WriteLine($"Graph image not found: {graphImagePath}");
            return;
        }

        // Process each PDF file in the input folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");
        foreach (string pdfPath in pdfFiles)
        {
            try
            {
                // Load the PDF document
                using (Document doc = new Document(pdfPath))
                {
                    // -------------------------------------------------
                    // Insert the predefined graph image on the first page
                    // -------------------------------------------------
                    ImageStamp graphStamp = new ImageStamp(graphImagePath)
                    {
                        Background = false,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Top,
                        XIndent = 0,               // Centered horizontally
                        YIndent = 20,              // 20 points from the top edge
                        Width = 400,               // Desired width of the graph
                        Height = 300               // Desired height of the graph
                    };
                    doc.Pages[1].AddStamp(graphStamp);    // Add to the first page

                    // -------------------------------------------------
                    // Insert a logo rectangle (as a TextStamp with background)
                    // -------------------------------------------------
                    TextStamp logoStamp = new TextStamp(logoText)
                    {
                        Background = true,                     // make it a background stamp
                        HorizontalAlignment = HorizontalAlignment.Left,
                        VerticalAlignment = VerticalAlignment.Bottom,
                        XIndent = 20,               // 20 points from the left edge
                        YIndent = 20,               // 20 points from the bottom edge
                        Width = 150,                // Width of the rectangle
                        Height = 50                 // Height of the rectangle
                    };
                    // Text appearance
                    logoStamp.TextState.Font = FontRepository.FindFont("Arial");
                    logoStamp.TextState.FontSize = 12;
                    logoStamp.TextState.ForegroundColor = Color.Black;
                    // Background colour of the rectangle – use TextState.BackgroundColor (TextStamp has no BackgroundColor property)
                    logoStamp.TextState.BackgroundColor = Color.LightGray;

                    doc.Pages[1].AddStamp(logoStamp);    // Add to the first page

                    // Save the modified PDF to the output folder
                    string outputPath = Path.Combine(outputFolder, Path.GetFileName(pdfPath));
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Processed: {Path.GetFileName(pdfPath)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing {pdfPath}: {ex.Message}");
            }
        }
    }
}
