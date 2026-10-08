using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    // Asynchronously adds a text stamp to all pages of a PDF.
    // The heavy PDF modification work is executed on a background thread via Task.Run.
    static async Task AddTextStampAsync(string inputPdfPath, string outputPdfPath, string stampText)
    {
        // Validate input file existence.
        if (!File.Exists(inputPdfPath))
            throw new FileNotFoundException($"Input PDF not found: {inputPdfPath}");

        // Run the synchronous Aspose.Pdf operations on a thread‑pool thread.
        await Task.Run(() =>
        {
            // Load the PDF document.
            Document doc = new Document(inputPdfPath);

            // Create a text stamp.
            TextStamp textStamp = new TextStamp(stampText)
            {
                // Configure appearance of the stamp.
                TextState = {
                    FontSize = 14,
                    FontStyle = FontStyles.Bold,
                    ForegroundColor = Color.FromRgb(0.8, 0.1, 0.1)
                },
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
                // Optional: rotate the stamp if desired.
                // Rotate = Rotation.Angle90
            };

            // Apply the stamp to every page.
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(textStamp);
            }

            // Save the modified PDF to the output path.
            doc.Save(outputPdfPath);
        });
    }

    static async Task Main(string[] args)
    {
        const string inputPath = "input.pdf";
        const string outputPath = "stamped_output.pdf";
        const string stampText = "Confidential";

        try
        {
            await AddTextStampAsync(inputPath, outputPath, stampText);
            Console.WriteLine($"Stamp added successfully. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
