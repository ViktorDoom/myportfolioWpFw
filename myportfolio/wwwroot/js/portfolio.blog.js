"use strict";

// DOM references
const blogContainer = document.getElementById("blog-container");
const blogStatus = document.getElementById("blog-status");

// API endpoint
const BLOG_URL = "/api/BlogPosts";

/**
 * Retrieves all blogposts from the ASP.NET Core API.
 *
 * @returns {Promise<Array>} Array of blogpost objects.
 */
async function fetchBlogPosts() {

    const response = await fetch(BLOG_URL);

    if (!response.ok) {
        throw new Error(
            `Failed to fetch blogposts: ${response.status}`
        );
    }

    const blogPosts = await response.json();

    if (!Array.isArray(blogPosts)) {
        throw new Error("Blogpost data must be an array.");
    }

    return blogPosts;
}

/**
 * Creates an HTML article for a blogpost.
 *
 * @param {Object} post - Blogpost data from the API.
 * @returns {HTMLElement} Blogpost article.
 */
function createBlogPost(post) {

    // Main article
    const article = document.createElement("article");
    article.classList.add("blog-post");

    // Header
    const header = document.createElement("header");
    header.classList.add("post-header");

    // Title
    const title = document.createElement("h2");
    title.textContent = post.title;

    // Timestamp
    const time = document.createElement("time");

    const createdAt = new Date(post.createdAt);

    time.dateTime = createdAt.toISOString();

    time.textContent = createdAt.toLocaleString("nl-NL", {
        day: "2-digit",
        month: "2-digit",
        year: "numeric",
        hour: "2-digit",
        minute: "2-digit"
    });

    // Assemble header
    header.appendChild(title);
    header.appendChild(time);

    // Content container
    const content = document.createElement("div");
    content.classList.add("post-content");

    const paragraph = document.createElement("p");
    paragraph.textContent = post.content;

    content.appendChild(paragraph);

    // Assemble article
    article.appendChild(header);
    article.appendChild(content);

    return article;
}

/**
 * Renders all blogposts.
 *
 * @param {Array} blogPosts - Array of blogposts.
 */
function renderBlogPosts(blogPosts) {

    // Remove previously rendered articles, preserving status
    blogContainer.querySelectorAll(".blog-post")
        .forEach(article => article.remove());

    const fragment = document.createDocumentFragment();

    blogPosts.forEach(post => {

        const article = createBlogPost(post);

        fragment.appendChild(article);

    });

    blogContainer.appendChild(fragment);
}

/**
 * Initializes the blog page.
 */
async function initializeBlog() {

    try {

        blogStatus.textContent = "Loading blogposts...";

        const blogPosts = await fetchBlogPosts();

        renderBlogPosts(blogPosts);

        blogStatus.textContent =
            blogPosts.length === 0
                ? "No blogposts available."
                : "";

    } catch (error) {

        console.error("Error loading blogposts:", error);

        blogStatus.textContent =
            "Unable to load blogposts. Please try again later.";
    }
}

// Start application
initializeBlog();