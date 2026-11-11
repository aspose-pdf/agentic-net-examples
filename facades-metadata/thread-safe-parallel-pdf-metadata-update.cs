using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input PDF files to be processed concurrently
        string[] inputFiles = { "doc1.pdf", "doc2.pdf", "doc3.pdf" };
        string outputDir = "Processed";
        Directory.CreateDirectory(outputDir);

        // Parallel processing – each thread works on its own PDF file
        Parallel.ForEach(inputFiles, inputPath =>
        {
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                return;
            }

            // Load the PDF in a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Thread‑local access to PdfFileInfo (no shared state)
                PdfFileInfo fileInfo = new PdfFileInfo(inputPath);

                // Example: read existing title and log it (thread‑safe console output)
                lock (Console.Out)
                {
                    Console.WriteLine($"Thread {Task.CurrentId}: Original Title = '{fileInfo.Title}'");
                }

                // Update the title metadata via the Document (this will be saved later)
                doc.Info.Title = Path.GetFileNameWithoutExtension(inputPath) + " – Processed";

                // Add a simple text stamp to each page
                TextStamp stamp = new TextStamp("Processed");
                stamp.HorizontalAlignment = HorizontalAlignment.Center;
                stamp.VerticalAlignment   = VerticalAlignment.Center;
                // Configure the TextState (read‑only property, modify its members directly)
                stamp.TextState.FontSize = 24;
                stamp.TextState.FontStyle = FontStyles.Bold;
                stamp.TextState.ForegroundColor = Aspose.Pdf.Color.FromRgb(0, 0, 1); // Blue

                foreach (Page page in doc.Pages)
                {
                    page.AddStamp(stamp);
                }

                // Save the modified PDF to a new file
                string outPath = Path.Combine(outputDir, Path.GetFileName(inputPath));
                doc.Save(outPath);

                // Re‑load PdfFileInfo for the newly saved file to verify the title
                PdfFileInfo updatedInfo = new PdfFileInfo(outPath);
                lock (Console.Out)
                {
                    Console.WriteLine($"Thread {Task.CurrentId}: Updated Title = '{updatedInfo.Title}'");
                }
            }

            // Optional: additional thread‑safe logging
            lock (Console.Out)
            {
                Console.WriteLine($"Thread {Task.CurrentId}: Finished processing {inputPath}");
            }
        });
    }
}
