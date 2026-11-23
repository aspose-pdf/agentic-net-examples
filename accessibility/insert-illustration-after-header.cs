using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

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

        using (Document doc = new Document(inputPath))
        {
            // Ensure the document is marked as tagged (works across library versions)
            var tagged = doc.TaggedContent;
            if (tagged != null)
            {
                var setTaggedMethod = tagged.GetType().GetMethod("SetTagged");
                if (setTaggedMethod != null)
                {
                    setTaggedMethod.Invoke(tagged, new object[] { true });
                }
                else
                {
                    var isTaggedProp = tagged.GetType().GetProperty("IsTagged");
                    if (isTaggedProp != null && isTaggedProp.CanWrite)
                    {
                        isTaggedProp.SetValue(tagged, true);
                    }
                }
            }

            ITaggedContent taggedContent = doc.TaggedContent;

            // Find the first HeaderElement in the structure tree
            HeaderElement header = null;
            var headers = taggedContent.RootElement.FindElements<HeaderElement>(true);
            if (headers.Count > 0)
                header = headers[0];

            if (header == null)
            {
                Console.Error.WriteLine("No HeaderElement found in the document.");
                return;
            }

            // Create an IllustrationElement using dynamic to avoid direct reference to the concrete TaggedContent class
            var illustration = ((dynamic)taggedContent).CreateIllustrationElement();
            illustration.AlternativeText = "Programmatically added illustration.";

            // Insert the illustration after the header by appending it to the same parent.
            StructureElement parent = header.ParentElement as StructureElement;
            if (parent != null)
            {
                parent.AppendChild(illustration);
            }
            else
            {
                // Fallback – add to the root element
                taggedContent.RootElement.AppendChild(illustration);
            }

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Updated PDF saved to '{outputPath}'.");
    }
}
