using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        // Folder containing source PDFs
        const string inputFolder = "input_pdfs";
        // Folder where tagged PDFs will be written
        const string outputFolder = "output_pdfs";
        // Suffix added to each output file name
        const string suffix = "_tagged.pdf";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Process every PDF file in the input folder
        foreach (string filePath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            try
            {
                // Load the PDF (lifecycle rule: use Document constructor)
                using (Document doc = new Document(filePath))
                {
                    // Enable automatic tagging (static global settings)
                    AutoTaggingSettings.Default.EnableAutoTagging = true;

                    // Access the tagged‑content API
                    ITaggedContent tagged = doc.TaggedContent;

                    // Set language and title (write‑only setters)
                    tagged.SetLanguage("en-US");
                    string title = Path.GetFileNameWithoutExtension(filePath);
                    tagged.SetTitle(title);

                    // Ensure a root structure element exists
                    StructureElement root = tagged.RootElement;

                    // If the document has no structure, add a simple paragraph
                    if (root.ChildElements.Count == 0)
                    {
                        ParagraphElement para = tagged.CreateParagraphElement();
                        para.SetText($"Document generated from {title}");
                        root.AppendChild(para); // AppendChild with one argument
                    }

                    // Persist any changes to the tagged structure
                    tagged.Save(); // ITaggedContent.Save()

                    // Build the output file name with the required suffix
                    string outputPath = Path.Combine(
                        outputFolder,
                        Path.GetFileNameWithoutExtension(filePath) + suffix);

                    // Save the PDF (lifecycle rule: use Document.Save(string))
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Processed: {Path.GetFileName(filePath)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{filePath}': {ex.Message}");
            }
        }
    }
}