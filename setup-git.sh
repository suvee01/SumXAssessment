#!/bin/bash

echo "🚀 Setting up Git Repository for Multi-Tenant Web API"
echo "=================================================="

# Check if git is already initialized
if [ -d ".git" ]; then
    echo "✅ Git repository already initialized"
else
    echo "📁 Initializing git repository..."
    git init
fi

# Add all files to staging
echo "📄 Adding all files to git..."
git add .

# Check git status
echo "📊 Current git status:"
git status --short

# Commit changes
echo "💾 Committing changes..."
git commit -m "Complete multi-tenant API implementation with user registration and employee CRUD

Features implemented:
- User registration with JWT authentication
- Employee CRUD operations with tenant isolation
- Admin-only tenant management
- Clean architecture with CQRS pattern
- Secure role-based authorization
- Comprehensive API documentation
- Testing files included"

echo ""
echo "🎯 Next Steps:"
echo "1. Create a new repository on GitHub"
echo "2. Copy the repository URL"
echo "3. Run the following commands:"
echo ""
echo "   git remote add origin https://github.com/yourusername/your-repo.git"
echo "   git branch -M main"
echo "   git push -u origin main"
echo ""
echo "✅ Your project is ready to be pushed to GitHub!"
echo ""
echo "📚 Documentation:"
echo "- API Documentation: API-Documentation.md"
echo "- Implementation Guide: IMPLEMENTATION_GUIDE.md"
echo "- Testing File: SumXAssessment/API-Testing.http"
echo ""
echo "🔐 Default Admin Login:"
echo "- Email: admin@system.com"
echo "- Password: Admin@123"