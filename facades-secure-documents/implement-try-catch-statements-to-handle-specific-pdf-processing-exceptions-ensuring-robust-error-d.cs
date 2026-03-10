using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // for text handling if needed

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string secondPdf = "second.pdf";
        const string concatenatedPdf = "merged.pdf";
        const string extractedTextPath = "extracted.txt";

        if (!File.Exists(inputPdf) || !File.Exists(secondPdf))
        {
            Console.Error.WriteLine("One or more input files are missing.");
            return;
        }

        // Load the first PDF document with proper disposal
        try
        {
            using (Document doc = new Document(inputPdf))
            {
                // ---------- Extract text using PdfExtractor ----------
                try
                {
                    PdfExtractor extractor = new PdfExtractor();
                    extractor.BindPdf(doc);
                    extractor.ExtractText();

                    // Save extracted text to a file
                    extractor.GetText(extractedTextPath);
                    Console.WriteLine($"Text extracted to '{extractedTextPath}'.");
                }
                catch (InvalidPdfFileFormatException ipffe)
                {
                    // Handles cases where the PDF file is corrupted or not a valid PDF
                    Console.Error.WriteLine($"Invalid PDF format: {ipffe.Message}");
                }
                catch (PdfException pdfEx)
                {
                    // General Aspose.Pdf processing errors
                    Console.Error.WriteLine($"PDF processing error: {pdfEx.Message}");
                }
                catch (Exception ex)
                {
                    // Any other unexpected errors
                    Console.Error.WriteLine($"Unexpected error during extraction: {ex.Message}");
                }

                // ---------- Concatenate with another PDF using PdfFileEditor ----------
                try
                {
                    PdfFileEditor editor = new PdfFileEditor();

                    // TryConcatenate returns false on failure; check LastException for details
                    bool concatenated = editor.TryConcatenate(inputPdf, secondPdf, concatenatedPdf);
                    if (concatenated)
                    {
                        Console.WriteLine($"PDFs concatenated successfully to '{concatenatedPdf}'.");
                    }
                    else
                    {
                        // Retrieve detailed error from the editor
                        Exception last = editor.LastException;
                        if (last != null)
                        {
                            Console.Error.WriteLine($"Concatenation failed: {last.Message}");
                            if (last.InnerException != null)
                                Console.Error.WriteLine($"Inner error: {last.InnerException.Message}");
                        }
                        else
                        {
                            Console.Error.WriteLine("Concatenation failed for an unknown reason.");
                        }
                    }
                }
                catch (PdfException pdfEx)
                {
                    // Handles errors thrown by PdfFileEditor methods (e.g., invalid source files)
                    Console.Error.WriteLine($"PDF file editor error: {pdfEx.Message}");
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Unexpected error during concatenation: {ex.Message}");
                }

                // ---------- Example of PDF/A conversion with specific exception handling ----------
                try
                {
                    // Convert the document to PDF/A-1B format
                    doc.Convert("conversion_log.xml", PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);
                    string pdfaPath = "output_pdfa.pdf";
                    doc.Save(pdfaPath);
                    Console.WriteLine($"PDF/A-1B saved to '{pdfaPath}'.");
                }
                catch (ConvertException convEx)
                {
                    // Specific conversion errors (e.g., compliance issues)
                    Console.Error.WriteLine($"Conversion error: {convEx.Message}");
                }
                catch (PdfException pdfEx)
                {
                    // General PDF processing errors during conversion
                    Console.Error.WriteLine($"PDF error during conversion: {pdfEx.Message}");
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Unexpected error during conversion: {ex.Message}");
                }
            }
        }
        catch (InvalidPdfFileFormatException ipffe)
        {
            // Handles errors when opening the initial document
            Console.Error.WriteLine($"Failed to open PDF (invalid format): {ipffe.Message}");
        }
        catch (PdfException pdfEx)
        {
            // Handles any other Aspose.Pdf related errors during document loading
            Console.Error.WriteLine($"Failed to load PDF: {pdfEx.Message}");
        }
        catch (Exception ex)
        {
            // Catch-all for any other exceptions (e.g., IO errors)
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}