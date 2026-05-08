using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputTxtPath = "output.txt";

        // Ensure the input PDF exists – if not, create a simple sample PDF.
        if (!File.Exists(inputPdfPath))
        {
            CreateSamplePdf(inputPdfPath);
            Console.WriteLine($"Sample PDF created at '{inputPdfPath}'.");
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        // Open a StreamWriter for the output text file
        using (StreamWriter writer = new StreamWriter(outputTxtPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
            {
                // Absorb paragraph structure from the current page
                ParagraphAbsorber absorber = new ParagraphAbsorber();
                absorber.Visit(pdfDoc.Pages[pageIndex]);

                // Each PageMarkup represents the logical structure of the page
                foreach (PageMarkup pageMarkup in absorber.PageMarkups)
                {
                    // Sections contain paragraphs
                    foreach (MarkupSection section in pageMarkup.Sections)
                    {
                        foreach (MarkupParagraph paragraph in section.Paragraphs)
                        {
                            // Each paragraph is composed of lines (list of text fragments)
                            foreach (var lineFragments in paragraph.Lines)
                            {
                                // Concatenate the text of all fragments in the line
                                string lineText = string.Empty;
                                foreach (TextFragment fragment in lineFragments)
                                {
                                    lineText += fragment.Text;
                                }
                                // Write the line preserving the line break
                                writer.WriteLine(lineText);
                            }
                            // Add an empty line to separate paragraphs
                            writer.WriteLine();
                        }
                    }
                }
            }
        }

        Console.WriteLine($"Paragraphs extracted to '{outputTxtPath}'.");
    }

    // Helper method to create a minimal PDF when the expected file is missing.
    private static void CreateSamplePdf(string path)
    {
        // On Windows we can safely use Aspose.Pdf's Document.Save which relies on GDI+.
        // On non‑Windows platforms (Linux/macOS) GDI+ (libgdiplus) may be missing, causing a
        // TypeInitializationException. In that case we fall back to writing a tiny hard‑coded
        // PDF byte stream that contains the same sample text.
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            Document doc = new Document();
            Page page = doc.Pages.Add();
            TextFragment tf = new TextFragment("First line of the sample paragraph.\nSecond line of the same paragraph.");
            page.Paragraphs.Add(tf);
            doc.Save(path);
        }
        else
        {
            // Minimal PDF content with one page and two lines of text.
            const string pdfContent = "%PDF-1.4\n" +
                "1 0 obj << /Type /Catalog /Pages 2 0 R >> endobj\n" +
                "2 0 obj << /Type /Pages /Kids [3 0 R] /Count 1 >> endobj\n" +
                "3 0 obj << /Type /Page /Parent 2 0 R /MediaBox [0 0 612 792] /Contents 4 0 R /Resources << /Font << /F1 5 0 R >> >> >> endobj\n" +
                "4 0 obj << /Length 112 >> stream\n" +
                "BT /F1 12 Tf 72 720 Td (First line of the sample paragraph.) Tj ET\n" +
                "BT /F1 12 Tf 72 704 Td (Second line of the same paragraph.) Tj ET\n" +
                "endstream endobj\n" +
                "5 0 obj << /Type /Font /Subtype /Type1 /BaseFont /Helvetica >> endobj\n" +
                "xref\n" +
                "0 6\n" +
                "0000000000 65535 f \n" +
                "0000000010 00000 n \n" +
                "0000000060 00000 n \n" +
                "0000000117 00000 n \n" +
                "0000000300 00000 n \n" +
                "0000000415 00000 n \n" +
                "trailer << /Size 6 /Root 1 0 R >>\n" +
                "startxref\n" +
                "527\n" +
                "%%EOF";
            File.WriteAllText(path, pdfContent);
        }
    }
}
