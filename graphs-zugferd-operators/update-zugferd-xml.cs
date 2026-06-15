using System;
using System.IO;
using System.Text;
using Aspose.Pdf;

namespace UpdateZugferdXmlExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Step 1: Create a simple PDF document.
            using (Document createDoc = new Document())
            {
                createDoc.Pages.Add();
                createDoc.Save("sample.pdf");
            }

            // Step 2: Embed an initial ZUGFeRD XML file.
            string oldXml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><Invoice><ID>INV-001</ID></Invoice>";
            using (MemoryStream oldXmlStream = new MemoryStream(Encoding.UTF8.GetBytes(oldXml)))
            {
                using (Document docWithOldXml = new Document("sample.pdf"))
                {
                    // Create a FileSpecification for the XML and assign its contents.
                    FileSpecification oldFileSpec = new FileSpecification("ZUGFeRD.xml", "ZUGFeRD XML");
                    oldFileSpec.Contents = oldXmlStream;
                    // Add the specification to the EmbeddedFiles collection.
                    docWithOldXml.EmbeddedFiles.Add(oldFileSpec);
                    docWithOldXml.Save("sample_with_zugferd.pdf");
                }
            }

            // Step 3: Replace the embedded ZUGFeRD XML with new content.
            string newXml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><Invoice><ID>INV-002</ID></Invoice>";
            using (MemoryStream newXmlStream = new MemoryStream(Encoding.UTF8.GetBytes(newXml)))
            {
                using (Document docToUpdate = new Document("sample_with_zugferd.pdf"))
                {
                    // Attempt to delete the existing embedded file if it exists.
                    try
                    {
                        docToUpdate.EmbeddedFiles.Delete("ZUGFeRD.xml");
                    }
                    catch (Exception)
                    {
                        // Ignore if the file does not exist.
                    }

                    // Create a new FileSpecification for the updated XML.
                    FileSpecification newFileSpec = new FileSpecification("ZUGFeRD.xml", "ZUGFeRD XML");
                    newFileSpec.Contents = newXmlStream;
                    docToUpdate.EmbeddedFiles.Add(newFileSpec);
                    docToUpdate.Save("updated.pdf");
                }
            }

            Console.WriteLine("ZUGFeRD XML attachment has been updated successfully.");
        }
    }
}
