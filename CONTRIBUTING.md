# Contributing

Thank you for contributing to the Aspose.PDF for .NET agentic examples repository.

This repository is **mostly generated** by the [Aspose.PDF Examples Generator](https://github.com/fahadadeel/aspose-pdf-net-api-v2). The generator produces `.cs` examples, the root and per-category `AGENTS.md` guides, every `index.json`, and the top-level `README.md`. Manual contributions are welcome for one-off fixes and additions, but should follow the rules below so the next regeneration does not undo your changes.

## How to add or fix one standalone `.cs` example

1. **Locate the right category folder.** Categories are kebab-case, e.g. `working-with-text/`. Pick the folder that best matches the API surface your example exercises.
2. **Create a single self-contained `.cs` file** in that folder. The filename must be kebab-case and end in `.cs`. The file must compile and run as a standalone .NET console application.
3. **Match the existing code style.** Use the patterns demonstrated in neighbouring examples: `using` blocks for all `IDisposable` types, fully-qualified `Aspose.Pdf.*` types to avoid `System.IO.Path` ambiguity, and `Console.Error.WriteLine` for error output.
4. **Do not include a `.csproj`.** The CI workflow synthesises one at validation time using the package version from `index.json` -- shipping a `.csproj` per example would conflict.
5. **Reference input files defensively.** If your example expects `input.pdf` (or similar), check existence with `File.Exists` and `Console.Error.WriteLine` on miss. The CI validator runs `dotnet run` with no input files available; runtime failure on missing input is non-blocking.

## Generated-file policy

The generator emits these files. **Do not hand-edit them** unless your task explicitly requires it -- the next regeneration overwrites manual changes:

- `AGENTS.md` (root and every category folder)
- `index.json` (root and every category folder)
- `README.md` (root)
- `.github/pull_request_template.md`
- `.github/copilot-instructions.md`

If you find a problem in one of these, **fix it upstream in the generator** (`git_ops/repo_docs.py` and friends in the generator repo), then regenerate. The exception: you may add new sections to `CONTRIBUTING.md` -- it is intentionally not regenerated word-for-word, the generator only seeds it.

## Local validation

Before opening a PR, validate the example builds and runs against the pinned NuGet version:

```bash
# From the category folder containing your example
mkdir -p /tmp/aspose-validate
cd /tmp/aspose-validate
dotnet new console --framework net10.0
cp /path/to/your-example.cs Program.cs
dotnet add package Aspose.PDF --version 26.7.0
dotnet build --nologo /p:WarningLevel=0
dotnet run --no-build || true   # runtime failure on missing input is OK
```

The build must complete with `Build succeeded` and zero errors. Runtime errors from missing input files (e.g. `input.pdf`) are tolerated by CI -- the build step is the gate.

## What GitHub Actions validates

The `.github/workflows/validate-pr.yml` workflow runs on every PR targeting `main` or any `release/*` branch:

1. Detects the NuGet version from `index.json`.
2. For each changed `.cs` file: synthesises a `.csproj`, `dotnet build` (**required** -- blocks merge), then `dotnet run` with a 15-second timeout (**informational** -- may fail on missing input).
3. Reports per-file build and run status in the PR check summary.

If `Build & Run changed examples` fails, look at the per-file output -- 99% of the time it is a missing `using`, a `Aspose.Pdf` vs `System.IO` ambiguity, or a method signature that does not exist on the pinned version.

## Pull request notes

Use [`.github/pull_request_template.md`](./.github/pull_request_template.md) (autofilled when you open a PR) to declare:

- Which examples or categories changed.
- The exact validation commands you ran and the outcome.
- Whether you actually ran the binary (vs. only compiling).
- Any input files your example needs that are not in the repo.
- Confirmation that you did **not** hand-edit generated metadata.

PRs that touch only generated files without a corresponding generator change will be asked to redirect upstream.
