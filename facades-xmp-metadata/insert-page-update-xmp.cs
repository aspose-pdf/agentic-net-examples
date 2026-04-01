using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document document = new Document(inputPath))
        {
            // Insert a new blank A4 page after the first page (position 2)
            // Use the overload that creates a blank page automatically.
            document.Pages.Insert(2);
            // Retrieve the newly inserted page to set its size.
            Page insertedPage = document.Pages[2];
            insertedPage.SetPageSize(PageSize.A4.Width, PageSize.A4.Height);

            // Prepare XMP metadata XML content
            string xmpXml = "<?xpacket begin='﻿' id='W5M0MpCehiHzreSzNTczkc9d'?>\n" +
                            "<x:xmpmeta xmlns:x='adobe:ns:meta/'>\n" +
                            "  <rdf:RDF xmlns:rdf='http://www.w3.org/1999/02/22-rdf-syntax-ns#'>\n" +
                            "    <rdf:Description rdf:about='' xmlns:dc='http://purl.org/dc/elements/1.1/'>\n" +
                            "      <dc:title>\n" +
                            "        <rdf:Alt>\n" +
                            "          <rdf:li xml:lang='x-default'>Updated Title</rdf:li>\n" +
                            "        </rdf:Alt>\n" +
                            "      </dc:title>\n" +
                            "      <dc:creator>\n" +
                            "        <rdf:Seq>\n" +
                            "          <rdf:li>John Doe</rdf:li>\n" +
                            "        </rdf:Seq>\n" +
                            "      </dc:creator>\n" +
                            "    </rdf:Description>\n" +
                            "  </rdf:RDF>\n" +
                            "</x:xmpmeta>\n" +
                            "<?xpacket end='w'?>";

            // Write the XMP XML into a memory stream and assign it to the document
            using (MemoryStream xmpStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(xmpXml)))
            {
                document.SetXmpMetadata(xmpStream);
            }

            // Save the modified PDF with the new page and updated XMP metadata
            document.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with new page and XMP metadata to '{outputPath}'.");
    }
}
