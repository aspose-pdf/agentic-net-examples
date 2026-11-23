using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;

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
            // Verify that the PDF is tagged
            if (doc.TaggedContent == null)
            {
                Console.WriteLine("Document is not tagged. Saving unchanged file.");
                doc.Save(outputPath);
                return;
            }

            // Access the tagged‑content API
            ITaggedContent tagged = doc.TaggedContent;
            // Use dynamic to avoid compile‑time dependency on concrete structure element types
            dynamic root = tagged.RootElement;

            // Find the first List element (tag "L") in the structure tree
            dynamic listElement = null;
            foreach (var child in root.ChildElements)
            {
                // The Tag property holds the PDF structure tag (e.g., "L" for List)
                if (child.Tag == "L")
                {
                    listElement = child;
                    break;
                }
            }

            if (listElement == null)
            {
                Console.WriteLine("No list elements found.");
                doc.Save(outputPath);
                return;
            }

            // Locate the first list item (any child of the list)
            dynamic itemToRemove = null;
            foreach (var item in listElement.ChildElements)
            {
                itemToRemove = item;
                break;
            }

            if (itemToRemove == null)
            {
                Console.WriteLine("List contains no items to remove.");
                doc.Save(outputPath);
                return;
            }

            // Remove the selected list item from its parent list
            listElement.RemoveChild(itemToRemove);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
