using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // <-- added for TextFragment

class Program
{
    static void Main()
    {
        // ---------------------------------------------------------------------
        // 1. Create a simple destination PDF completely in memory.
        // ---------------------------------------------------------------------
        using (var destDoc = new Document())
        {
            // Add a single page with some sample text.
            var destPage = destDoc.Pages.Add();
            destPage.Paragraphs.Add(new TextFragment("Destination PDF – Page 1"));

            using (var destStream = new MemoryStream())
            {
                destDoc.Save(destStream);
                destStream.Position = 0; // reset for reading

                // -----------------------------------------------------------------
                // 2. Create a simple source PDF (the pages we want to insert).
                // -----------------------------------------------------------------
                using (var srcDoc = new Document())
                {
                    // Add two pages – these will be inserted into the destination.
                    var srcPage1 = srcDoc.Pages.Add();
                    srcPage1.Paragraphs.Add(new TextFragment("Source PDF – Page 1"));
                    var srcPage2 = srcDoc.Pages.Add();
                    srcPage2.Paragraphs.Add(new TextFragment("Source PDF – Page 2"));

                    using (var srcStream = new MemoryStream())
                    {
                        srcDoc.Save(srcStream);
                        srcStream.Position = 0; // reset for reading

                        // -------------------------------------------------------------
                        // 3. Insert the selected pages from the source into the destination.
                        // -------------------------------------------------------------
                        int insertLocation = 1;                 // insert at the beginning
                        int[] pageNumbers = new int[] { 1, 2 }; // pages from source to insert

                        using (var outputStream = new MemoryStream())
                        {
                            var editor = new PdfFileEditor();
                            bool success = editor.Insert(
                                destStream,        // destination PDF stream (read‑only)
                                insertLocation,    // position in destination where pages will be inserted
                                srcStream,         // source PDF stream
                                pageNumbers,       // pages to take from source
                                outputStream);     // resulting PDF stream

                            if (!success)
                            {
                                Console.Error.WriteLine("Page insertion failed.");
                                return;
                            }

                            // The outputStream now contains the merged PDF.
                            outputStream.Position = 0; // reset before further use

                            // Optional: write the merged PDF to a file for verification.
                            using (var fileOut = new FileStream("merged_output.pdf", FileMode.Create, FileAccess.Write))
                            {
                                outputStream.CopyTo(fileOut);
                            }

                            Console.WriteLine("Pages inserted successfully. Result is available in the MemoryStream.");
                        }
                    }
                }
            }
        }
    }
}
