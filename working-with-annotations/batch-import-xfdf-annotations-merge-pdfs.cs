using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;

class BatchXfdfImporter
{
    static void Main()
    {
        // Directory containing PDF files and their matching XFDF files
        const string inputDirectory = @"C:\InputFiles";
        // Path for the final merged PDF
        const string outputPdfPath = @"C:\Output\merged_output.pdf";

        if (!Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDirectory}");
            return;
        }

        // Collect all PDF files in the directory
        string[] pdfFiles = Directory.GetFiles(inputDirectory, "*.pdf", SearchOption.TopDirectoryOnly);
        if (pdfFiles.Length == 0)
        {
            Console.Error.WriteLine("No PDF files found in the input directory.");
            return;
        }

        // List to hold loaded documents
        List<Document> documents = new List<Document>();

        try
        {
            // Load each PDF, import its XFDF annotations (if present), and keep the document alive
            foreach (string pdfPath in pdfFiles)
            {
                // Load the PDF document
                Document doc = new Document(pdfPath);

                // Determine the corresponding XFDF file (same name, .xfdf extension)
                string xfdfPath = Path.ChangeExtension(pdfPath, ".xfdf");
                if (File.Exists(xfdfPath))
                {
                    // Import annotations from the XFDF file into the PDF document
                    doc.ImportAnnotationsFromXfdf(xfdfPath);
                }
                else
                {
                    Console.WriteLine($"No XFDF file for '{Path.GetFileName(pdfPath)}'; skipping import.");
                }

                // Keep the document for later merging
                documents.Add(doc);
            }

            // Merge all processed documents into a single PDF
            // The static MergeDocuments method returns a new Document containing all pages
            Document mergedDocument = Document.MergeDocuments(documents.ToArray());

            // Save the merged PDF
            mergedDocument.Save(outputPdfPath);
            mergedDocument.Dispose();

            Console.WriteLine($"Merged PDF saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during processing: {ex.Message}");
        }
        finally
        {
            // Ensure all individual documents are disposed to release resources
            foreach (Document doc in documents)
            {
                doc.Dispose();
            }
        }
    }
}