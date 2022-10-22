## Theory

**The API is versioned, meaning that it is backwards-compatible.**

The API evolves over time, and so do endpoints.

When non-breaking changes happen, your application needs to tolerate new objects being added into the JSON.

**If a breaking change happens, the updated endpoint(s) with the breaking change will recieve a new version under a new route.**

---

If you want to know which route is newest, you can fetch `/api/latest`. *Do not expect all endpoints to be accessible under the newest route.*

The API version found in `/api/version` follows the semantic versioning scheme.

## Example URLs

*(Assuming `/api/latest` returns `v2` or higher)*

- `/api/v1/gelbooru/[...]/50;50`
- `/api/v2/safebooru/[...]/50;100`