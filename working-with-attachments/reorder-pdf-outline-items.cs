using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "portfolio.pdf";
        const string outputPath = "portfolio_reordered.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (creation rule is applied via the Document constructor)
        using (Document doc = new Document(inputPath))
        {
            // If there are no outline items, just save the original document
            if (doc.Outlines == null || doc.Outlines.Count == 0)
            {
                Console.WriteLine("No outline items to reorder.");
                doc.Save(outputPath); // save rule
                return;
            }

            // ------------------------------------------------------------
            // 1. Capture current outline items (including hierarchy)
            // ------------------------------------------------------------
            var originalRoots = new List<dynamic>();
            foreach (var root in doc.Outlines) // root is a dynamic OutlineItem
            {
                originalRoots.Add(root);
            }

            // ------------------------------------------------------------
            // 2. Delete all existing outline items (delete rule)
            // ------------------------------------------------------------
            doc.Outlines.Delete(); // removes every outline entry

            // ------------------------------------------------------------
            // 3. Re‑add items in the desired order.
            //    Example: reverse the top‑level order; child order is preserved.
            // ------------------------------------------------------------
            for (int i = originalRoots.Count - 1; i >= 0; i--)
            {
                dynamic srcRoot = originalRoots[i];
                dynamic newRoot = doc.Outlines.Add(srcRoot.Title);

                // Preserve navigation (Action or Destination)
                if (srcRoot.Action != null)
                    newRoot.Action = srcRoot.Action;
                else if (srcRoot.Destination != null)
                    newRoot.Destination = srcRoot.Destination;

                // Recursively copy any child outline items
                CloneChildren(srcRoot, newRoot);
            }

            // Save the reordered PDF (save rule)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Reordered PDF saved to '{outputPath}'.");
    }

    // Recursively copies child outline items from source to target using dynamic typing
    static void CloneChildren(dynamic sourceParent, dynamic targetParent)
    {
        foreach (var child in sourceParent) // child is a dynamic OutlineItem
        {
            dynamic newChild = targetParent.Add(child.Title);

            if (child.Action != null)
                newChild.Action = child.Action;
            else if (child.Destination != null)
                newChild.Destination = child.Destination;

            // Continue copying deeper levels
            CloneChildren(child, newChild);
        }
    }
}
