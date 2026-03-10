using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Enable System.Drawing support on non‑Windows platforms (requires libgdiplus on Linux/macOS)
        // This must be set before any Aspose.Pdf types that internally use System.Drawing are touched.
        AppContext.SetSwitch("System.Drawing.EnableUnixSupport", true);

        // Input PDF file path
        const string inputPdf = "input.pdf";
        // Output EPUB file path
        const string outputEpub = "output.epub";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Ensure the document has at least one page
                if (pdfDoc.Pages.Count < 1)
                {
                    Console.Error.WriteLine("The PDF does not contain any pages.");
                    return;
                }

                // Create a text annotation on the first page
                Page firstPage = pdfDoc.Pages[1]; // 1‑based indexing

                // Define the annotation rectangle (llx, lly, urx, ury)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

                // Instantiate the annotation
                TextAnnotation textAnn = new TextAnnotation(firstPage, rect)
                {
                    Title    = "Note",
                    Contents = "This is an extra annotation added via Aspose.Pdf.",
                    Color    = Aspose.Pdf.Color.Yellow,
                    Open     = true,
                    Icon     = TextIcon.Note
                };

                // Add the annotation to the page's annotation collection
                firstPage.Annotations.Add(textAnn);

                // Prepare EPUB save options
                EpubSaveOptions epubOptions = new EpubSaveOptions
                {
                    // Optional: set the EPUB title
                    Title = "PDF with Extra Annotations",
                    // ContentRecognitionMode defaults to Flow; can be changed if needed
                };

                // Save the document as EPUB using the explicit save options
                pdfDoc.Save(outputEpub, epubOptions);
            }

            Console.WriteLine($"EPUB file created successfully: {outputEpub}");
        }
        catch (Exception ex)
        {
            // Provide a clear message when System.Drawing cannot be initialized (e.g., missing libgdiplus on Linux/macOS)
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
            if (ex.InnerException != null)
                Console.Error.WriteLine($"Inner exception: {ex.InnerException.Message}");
            Console.Error.WriteLine("If you are running on Linux/macOS, ensure that the 'libgdiplus' package is installed.\n" +
                                 "On Debian/Ubuntu you can install it with: sudo apt-get install -y libgdiplus\n" +
                                 "On RHEL/CentOS: sudo yum install -y libgdiplus\n" +
                                 "On macOS with Homebrew: brew install mono-libgdiplus");
        }
    }
}
