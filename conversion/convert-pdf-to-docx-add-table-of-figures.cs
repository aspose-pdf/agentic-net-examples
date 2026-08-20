using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputDocxPath = "output.docx";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Prepare a list to hold figure descriptions (captions)
            List<string> figureCaptions = new List<string>();

            // Iterate through all pages and collect images
            int pageNumber = 1;
            foreach (Page page in pdfDoc.Pages)
            {
                foreach (XImage img in page.Resources.Images)
                {
                    // Generate a simple caption for the image
                    string caption = $"Image on page {pageNumber}";
                    // Store the caption for later table creation
                    figureCaptions.Add(caption);

                    // Optionally set alternative text directly on the image resource
                    img.TrySetAlternativeText(caption, page);
                }
                pageNumber++;
            }

            // Access the tagged content API
            ITaggedContent tagged = pdfDoc.TaggedContent;

            // Create a table of figures using the tagged content factory
            StructureElement root = tagged.RootElement;

            // Table element
            TableElement table = tagged.CreateTableElement();
            table.AlternativeText = "Table of Figures";
            root.AppendChild(table);

            // Table header
            TableTHeadElement thead = tagged.CreateTableTHeadElement();
            table.AppendChild(thead);
            TableTRElement headerRow = tagged.CreateTableTRElement();
            thead.AppendChild(headerRow);

            TableTHElement th1 = tagged.CreateTableTHElement();
            th1.SetText("Figure");
            headerRow.AppendChild(th1);

            TableTHElement th2 = tagged.CreateTableTHElement();
            th2.SetText("Description");
            headerRow.AppendChild(th2);

            // Table body
            TableTBodyElement tbody = tagged.CreateTableTBodyElement();
            table.AppendChild(tbody);

            // Populate table rows with figure numbers and captions
            for (int i = 0; i < figureCaptions.Count; i++)
            {
                TableTRElement dataRow = tagged.CreateTableTRElement();
                tbody.AppendChild(dataRow);

                TableTDElement tdNum = tagged.CreateTableTDElement();
                tdNum.SetText($"Figure {i + 1}");
                dataRow.AppendChild(tdNum);

                TableTDElement tdDesc = tagged.CreateTableTDElement();
                tdDesc.SetText(figureCaptions[i]);
                dataRow.AppendChild(tdDesc);
            }

            // Convert the PDF to DOCX with appropriate save options
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                // Export as DOCX
                Format = DocSaveOptions.DocFormat.DocX,
                // Use Flow mode for better editability
                Mode = DocSaveOptions.RecognitionMode.Flow,
                // Enable bullet recognition (optional)
                RecognizeBullets = true
            };

            pdfDoc.Save(outputDocxPath, saveOptions);
        }

        Console.WriteLine($"PDF converted to DOCX with a table of figures: {outputDocxPath}");
    }
}