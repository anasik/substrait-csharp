## Summary

No workflow is registered on this repo's default branch, so GitHub creates no `pull_request` check runs for incoming PRs. #5 adds its own copy of this file, but because that PR's branch is named `main`, the workflow's `push` trigger has only ever run it on the contributor's fork — never here.

This lands the workflow in its **final** form, so #5 doesn't need to touch it. Three things have to come with it for the four steps to be green:

- `.editorconfig` — otherwise `dotnet format` applies its 4-space default and fails against this repo's 2-space style.
- the `.slnx` migration the workflow refers to.
- a test project, so `dotnet test` has something to run.

Verified on SDK 10.0.302 — restore, format check, build and test all pass. Existing sources already satisfy the `.editorconfig`, so no reformatting was needed.

The tests are the two `SubstraitRelVisitor` cases from #5, adjusted for `main`'s namespaces: real assertions against behaviour that exists today rather than a placeholder, and they converge with #5 rather than leaving a tautology to delete.

## Relationship to #5

This overlaps #5, which adds the same four files. After this merges, #5 rebases and drops its copies, keeping only the type system — the split suggested in review there.

🤖 Generated with AI
