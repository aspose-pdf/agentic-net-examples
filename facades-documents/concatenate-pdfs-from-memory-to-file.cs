using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Create two sample PDFs entirely in memory.
        using (MemoryStream pdf1 = CreateSamplePdf("First PDF"))
        using (MemoryStream pdf2 = CreateSamplePdf("Second PDF"))
        {
            // Reset stream positions before concatenation.
            pdf1.Position = 0;
            pdf2.Position = 0;

            const string outputFile = "merged.pdf";

            // Write the concatenated result directly to a file stream.
            using (FileStream outStream = new FileStream(outputFile, FileMode.Create, FileAccess.Write))
            {
                PdfFileEditor editor = new PdfFileEditor
                {
                    // The editor will close the input streams after concatenation.
                    CloseConcatenatedStreams = true
                };

                editor.Concatenate(new Stream[] { pdf1, pdf2 }, outStream);
            }
        }

        Console.WriteLine($"PDFs have been concatenated and saved to 'merged.pdf'.");
    }

    // Helper that builds a minimal PDF in memory containing a single text fragment.
    static MemoryStream CreateSamplePdf(string text)
    {
        Document doc = new Document();
        Page page = doc.Pages.Add();
        page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment(text));
        MemoryStream ms = new MemoryStream();
        doc.Save(ms);
        return ms;
    }
}