using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // Added for TextFragment

class Program
{
    static void Main()
    {
        // Paths of the split PDF files that need to be concatenated.
        // In a real scenario these could be collected dynamically.
        string[] splitFiles = new string[]
        {
            "split_part_1.pdf",
            "split_part_2.pdf",
            "split_part_3.pdf"
            // add more files as needed
        };

        // ------------------------------------------------------------
        // STEP 1: Ensure the split PDFs exist. In the sandbox there are
        // no pre‑existing files, so we create minimal PDFs on‑the‑fly.
        // ------------------------------------------------------------
        for (int i = 0; i < splitFiles.Length; i++)
        {
            if (!File.Exists(splitFiles[i]))
            {
                using (Document doc = new Document())
                {
                    Page page = doc.Pages.Add();
                    // Add a simple text fragment so each part is identifiable.
                    TextFragment tf = new TextFragment($"This is part {i + 1}");
                    page.Paragraphs.Add(tf);
                    doc.Save(splitFiles[i]);
                }
            }
        }

        // Destination file for the final concatenated PDF.
        const string outputFile = "merged_result.pdf";

        if (splitFiles.Length == 0)
        {
            Console.Error.WriteLine("No input files provided.");
            return;
        }

        // Create a temporary working file based on the first split part.
        string workingFile = Path.GetTempFileName();
        File.Copy(splitFiles[0], workingFile, true);

        // PdfFileEditor provides the Insert method (InsertPages functionality).
        PdfFileEditor editor = new PdfFileEditor();

        // Iterate over the remaining split parts and insert them sequentially.
        for (int i = 1; i < splitFiles.Length; i++)
        {
            string partFile = splitFiles[i];

            // Determine the insertion position – after the last page of the current working file.
            int insertLocation;
            using (Document baseDoc = new Document(workingFile))
            {
                // Aspose.Pdf.Facades.PdfFileEditor.Insert expects a 1‑based page number
                // after which the new pages will be inserted. To append to the end we
                // use the current page count (no +1). This maps to the internal zero‑
                // based List.Insert index and prevents an ArgumentOutOfRangeException.
                insertLocation = baseDoc.Pages.Count; // correct index for appending
            }

            // Determine the range of pages to take from the part file (the whole document).
            int endPage;
            using (Document partDoc = new Document(partFile))
            {
                endPage = partDoc.Pages.Count;
            }

            // Perform the insertion. The result is written to a new temporary file.
            string tempOutput = Path.GetTempFileName();
            bool success = editor.Insert(
                workingFile,          // input PDF (current aggregate)
                insertLocation,       // position where pages will be inserted (1‑based, after this page)
                partFile,             // source PDF to insert
                1,                    // start page in source PDF
                endPage,              // end page in source PDF
                tempOutput);          // output PDF

            if (!success)
            {
                Console.Error.WriteLine($"Failed to insert pages from '{partFile}'.");
                // Clean up temporary files before exiting.
                File.Delete(workingFile);
                File.Delete(tempOutput);
                return;
            }

            // Replace the working file with the newly created output for the next iteration.
            File.Delete(workingFile);
            File.Move(tempOutput, workingFile);
        }

        // Move the final aggregated PDF to the desired output location.
        File.Copy(workingFile, outputFile, true);
        File.Delete(workingFile);

        Console.WriteLine($"All parts concatenated successfully. Output saved to '{outputFile}'.");
    }
}
