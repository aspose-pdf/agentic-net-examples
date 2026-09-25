using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        // Folder containing PDFs to process
        const string inputFolder = @"C:\PdfFolder";
        // Suffix to add to processed files
        const string suffix = "_tagged";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Folder not found: {inputFolder}");
            return;
        }

        // Process each PDF file in the folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            try
            {
                // Open the PDF document
                using (Document doc = new Document(pdfPath))
                {
                    // Access the tagged content API
                    ITaggedContent taggedContent = doc.TaggedContent;

                    // Set language and title (title derived from file name)
                    taggedContent.SetLanguage("en-US");
                    string title = Path.GetFileNameWithoutExtension(pdfPath);
                    taggedContent.SetTitle(title);

                    // Get the root structure element (no cast required)
                    StructureElement root = taggedContent.RootElement;

                    // Add a simple paragraph element if the document has no content
                    // (this demonstrates adding a tag; in real scenarios you would
                    //  create appropriate structure based on the PDF content)
                    ParagraphElement para = taggedContent.CreateParagraphElement();
                    para.SetText("Document has been auto‑tagged for accessibility.");
                    root.AppendChild(para);

                    // Build output file name with suffix
                    string dir = Path.GetDirectoryName(pdfPath);
                    string fileName = Path.GetFileNameWithoutExtension(pdfPath);
                    string outPath = Path.Combine(dir, $"{fileName}{suffix}.pdf");

                    // Save the modified PDF
                    doc.Save(outPath);
                    Console.WriteLine($"Tagged PDF saved: {outPath}");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}