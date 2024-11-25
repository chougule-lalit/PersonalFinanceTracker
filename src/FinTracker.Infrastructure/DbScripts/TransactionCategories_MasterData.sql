INSERT INTO "TransactionCategories" ("Id", "Name", "Description", "Type", "IsDefault", "CreatedAt", "CreatedById", "LastModifiedAt", "LastModifiedById", "DeletedAt", "DeletedById", "IsDeleted")
VALUES
    -- Income Categories
    ('71a417c4-8924-4a98-8a51-271a42c4e4a1', 'Salary', 'Regular employment income', 0, true, NOW(), NULL, NOW(), NULL, NULL, NULL, false),
    ('72a417c4-8924-4a98-8a51-271a42c4e4a2', 'Freelance', 'Independent contractor income', 0, true, NOW(), NULL, NOW(), NULL, NULL, NULL, false),
    ('73a417c4-8924-4a98-8a51-271a42c4e4a3', 'Investments', 'Returns from investments, dividends, capital gains', 0, true, NOW(), NULL, NOW(), NULL, NULL, NULL, false),
    ('74a417c4-8924-4a98-8a51-271a42c4e4a4', 'Rental', 'Income from property rentals', 0, true, NOW(), NULL, NOW(), NULL, NULL, NULL, false),
    ('75a417c4-8924-4a98-8a51-271a42c4e4a5', 'Interest', 'Interest earned from savings accounts', 0, true, NOW(), NULL, NOW(), NULL, NULL, NULL, false),
    ('76a417c4-8924-4a98-8a51-271a42c4e4a6', 'Bonus', 'Work bonuses and incentives', 0, true, NOW(), NULL, NOW(), NULL, NULL, NULL, false),
    ('77a417c4-8924-4a98-8a51-271a42c4e4a7', 'Gift', 'Monetary gifts received', 0, true, NOW(), NULL, NOW(), NULL, NULL, NULL, false),
    ('78a417c4-8924-4a98-8a51-271a42c4e4a8', 'Other Income', 'Miscellaneous income sources', 0, true, NOW(), NULL, NOW(), NULL, NULL, NULL, false),

    -- Expense Categories - Housing & Utilities
    ('81a417c4-8924-4a98-8a51-271a42c4e4b1', 'Rent/Mortgage', 'Monthly housing payments', 1, true, NOW(), NULL, NOW(), NULL, NULL, NULL, false),
    ('82a417c4-8924-4a98-8a51-271a42c4e4b2', 'Electricity', 'Electricity bills', 1, true, NOW(), NULL, NOW(), NULL, NULL, NULL, false),
    ('83a417c4-8924-4a98-8a51-271a42c4e4b3', 'Water', 'Water and sewage bills', 1, true, NOW(), NULL, NOW(), NULL, NULL, NULL, false),
    ('84a417c4-8924-4a98-8a51-271a42c4e4b4', 'Internet', 'Internet service bills', 1, true, NOW(), NULL, NOW(), NULL, NULL, NULL, false),
    ('85a417c4-8924-4a98-8a51-271a42c4e4b5', 'Phone', 'Mobile and landline phone bills', 1, true, NOW(), NULL, NOW(), NULL, NULL, NULL, false),

    -- Transportation
    ('86a417c4-8924-4a98-8a51-271a42c4e4b6', 'Fuel', 'Vehicle fuel expenses', 1, true, NOW(), NULL, NOW(), NULL, NULL, NULL, false),
    ('87a417c4-8924-4a98-8a51-271a42c4e4b7', 'Public Transport', 'Bus, train, and other public transportation', 1, true, NOW(), NULL, NOW(), NULL, NULL, NULL, false),
    ('88a417c4-8924-4a98-8a51-271a42c4e4b8', 'Vehicle Maintenance', 'Car repairs and maintenance', 1, true, NOW(), NULL, NOW(), NULL, NULL, NULL, false),

    -- Food & Living
    ('89a417c4-8924-4a98-8a51-271a42c4e4b9', 'Groceries', 'Food and household supplies', 1, true, NOW(), NULL, NOW(), NULL, NULL, NULL, false),
    ('90a417c4-8924-4a98-8a51-271a42c4e4c1', 'Restaurants', 'Dining out expenses', 1, true, NOW(), NULL, NOW(), NULL, NULL, NULL, false),
    ('91a417c4-8924-4a98-8a51-271a42c4e4c2', 'Home Maintenance', 'House repairs and maintenance', 1, true, NOW(), NULL, NOW(), NULL, NULL, NULL, false),

    -- Health & Wellness
    ('92a417c4-8924-4a98-8a51-271a42c4e4c3', 'Healthcare', 'Medical and dental expenses', 1, true, NOW(), NULL, NOW(), NULL, NULL, NULL, false),
    ('93a417c4-8924-4a98-8a51-271a42c4e4c4', 'Fitness', 'Gym memberships and equipment', 1, true, NOW(), NULL, NOW(), NULL, NULL, NULL, false),
    ('94a417c4-8924-4a98-8a51-271a42c4e4c5', 'Pharmacy', 'Medications and pharmacy items', 1, true, NOW(), NULL, NOW(), NULL, NULL, NULL, false),

    -- Entertainment & Leisure
    ('95a417c4-8924-4a98-8a51-271a42c4e4c6', 'Entertainment', 'Movies, events, and activities', 1, true, NOW(), NULL, NOW(), NULL, NULL, NULL, false),
    ('96a417c4-8924-4a98-8a51-271a42c4e4c7', 'Shopping', 'Clothing and personal items', 1, true, NOW(), NULL, NOW(), NULL, NULL, NULL, false),
    ('97a417c4-8924-4a98-8a51-271a42c4e4c8', 'Travel', 'Vacation and travel expenses', 1, true, NOW(), NULL, NOW(), NULL, NULL, NULL, false),

    -- Financial & Insurance
    ('98a417c4-8924-4a98-8a51-271a42c4e4c9', 'Insurance', 'All types of insurance premiums', 1, true, NOW(), NULL, NOW(), NULL, NULL, NULL, false),
    ('99a417c4-8924-4a98-8a51-271a42c4e4d1', 'Taxes', 'Tax payments', 1, true, NOW(), NULL, NOW(), NULL, NULL, NULL, false),
    ('9aa417c4-8924-4a98-8a51-271a42c4e4d2', 'Banking Fees', 'Bank charges and fees', 1, true, NOW(), NULL, NOW(), NULL, NULL, NULL, false),

    -- Education & Professional
    ('9ba417c4-8924-4a98-8a51-271a42c4e4d3', 'Education', 'Tuition and education expenses', 1, true, NOW(), NULL, NOW(), NULL, NULL, NULL, false),
    ('9ca417c4-8924-4a98-8a51-271a42c4e4d4', 'Professional Development', 'Work-related training and certifications', 1, true, NOW(), NULL, NOW(), NULL, NULL, NULL, false),

    -- Miscellaneous
    ('9da417c4-8924-4a98-8a51-271a42c4e4d5', 'Gifts Given', 'Gifts for others', 1, true, NOW(), NULL, NOW(), NULL, NULL, NULL, false),
    ('9ea417c4-8924-4a98-8a51-271a42c4e4d6', 'Donations', 'Charitable contributions', 1, true, NOW(), NULL, NOW(), NULL, NULL, NULL, false),
    ('9fa417c4-8924-4a98-8a51-271a42c4e4d7', 'Other Expenses', 'Miscellaneous expenses', 1, true, NOW(), NULL, NOW(), NULL, NULL, NULL, false);