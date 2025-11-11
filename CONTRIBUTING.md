
# Contributing Guide

Thank you for your interest in contributing!  
All contributions must be made through **forks and pull requests** — please **do not create branches directly on this repository**.

This guide walks you through the standard GitHub workflow for proposing changes.

---

## 🪄 Overview

You’ll:

1. **Fork** this repository to your own GitHub account.  
2. **Clone** your fork locally.  
3. **Create a branch** on your fork for your work.  
4. **Commit and push** your changes.  
5. **Open a Pull Request (PR)** to propose your changes here.  

---

## 🚀 Steps to Contribute

### 1. Fork the Repository
Click the **Fork** button in the top-right corner of this repository to create your own copy under your GitHub account.  
Your fork will live on your account.

### 2. Clone Your Fork
Clone your fork to your local machine:
```bash
git clone https://github.com/RealWorldDevelopers/RWD.Toolbox.PasswordGenerator.git
cd <repo-name>
````

### 3. Add the Upstream Remote 
Add the original repository as a remote called upstream:

```bash
git remote add upstream https://github.com/<original-author>/<repo-name>.git
git remote -v
```

### 4. Keep Your Fork Up to Date  
Before starting any work, always sync with the latest main branch:

```bash
git checkout main
git fetch upstream
git rebase upstream/main   # or git merge upstream/main
git push origin main
```

### 5. Create a Feature Branch (on Your Fork)  
Make a branch for your work — do **not** commit directly to main:

```bash
git checkout -b feature/my-change
```

### 6. Make and Commit Your Changes
Edit files, then stage and commit:

```bash
git add .
git commit -m "Describe your change briefly"
```

### 7. Push Your Branch  
Push your new branch up to your fork:

```bash
git push origin feature/my-change
```

### 8. Open a Pull Request  
On GitHub, go to **your fork** and click **Compare & pull request**.

Target branch: main of the **original repository**.
Describe your changes clearly, and submit the PR for review.

---

## 🔄 Keeping Your Branch Updated  
If updates happen in main while you’re working:

```bash
# Update your local main
git checkout main
git fetch upstream
git rebase upstream/main
git push origin main

# Rebase your feature branch
git checkout feature/my-change
git rebase main
# Resolve conflicts if needed:
git add <files>
git rebase --continue
git push origin feature/my-change --force
```

---

## **🧩 Code Review and Merge**

- The maintainers will review your PR.
- You may be asked to make updates.
- Once approved, your PR will be merged.
- After merge, delete your branch both locally and remotely.

---

## **🧭 Key Rules**

- ✅ Always work in your **own fork**.
- 🚫 Never create or push branches directly to this repository.
- 💬 Keep PRs focused — one topic per PR is best.
- 🧹 Keep commits clean and descriptive.

---

### **Ready-to-Copy Sync Commands**

```bash
# Sync fork and feature branch
git checkout main
git fetch upstream
git rebase upstream/main
git push origin main
git checkout feature/my-change
git rebase main
git push origin feature/my-change --force
```

---

> For more help, see the official GitHub docs:
> [Contributing to a project](https://docs.github.com/en/get-started/exploring-projects-on-github/contributing-to-a-project)
