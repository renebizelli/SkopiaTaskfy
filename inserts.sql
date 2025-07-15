-- Users
SET IDENTITY_INSERT USERS ON

INSERT INTO [Users] (Id, ExternalId, Name, Email, Role) VALUES
(1, 'C98F144D-291C-4425-B234-0AB7625318FA', 'René Bizelli', 'rene.bizelli@company.com', 2),
(2, '39B07325-81A5-4B23-B8D3-83800CB33C63', 'Julia Martins', 'julia.martins@company.com', 1),
(3, '3AFD291D-6C94-41B2-90AD-7E18B0983EC6', 'Carlos Souza', 'carlos.souza@company.com', 1),
(4, '4666F13C-63A8-4007-9765-C29B10323E94', 'Ana Paula', 'ana.paula@company.com', 2),
(5, '4336A98F-A3C2-4473-92A9-41192B11538B', 'Marcos Lima', 'marcos.lima@company.com', 1);

SET IDENTITY_INSERT USERS OFF


SET IDENTITY_INSERT [Projects] ON

-- Projects
INSERT INTO [Projects] (Id, ExternalId, Name, TaskLimit, Active, CreatedAt) VALUES
(1, NEWID(), 'Taskfy Platform', 50, 1, GETDATE()),
(2, NEWID(), 'Mobile App Redesign', 40, 1, GETDATE()),
(3, NEWID(), 'API Integration', 30, 1, GETDATE()),
(4, NEWID(), 'Marketing Website', 25, 1, GETDATE()),
(5, NEWID(), 'Internal Tools', 20, 1, GETDATE()),
(6, NEWID(), 'Customer Portal', 35, 1, GETDATE()),
(7, NEWID(), 'Analytics Dashboard', 30, 1, GETDATE()),
(8, NEWID(), 'Onboarding Flow', 15, 1, GETDATE()),
(9, NEWID(), 'Notification Service', 20, 1, GETDATE()),
(10, NEWID(), 'Billing System', 25, 1, GETDATE());

SET IDENTITY_INSERT [Projects] OFF

INSERT INTO [ProjectsUsers] (ProjectId, UserId) VALUES
(1, 1), -- René Bizelli
(1, 2), -- Julia Martins
(1, 3), -- Carlos Souza
(1, 4), -- Ana Paula
(1, 5), -- Marcos Lima
(2, 1),
(3, 2),
(3, 4),
(4, 2),
(4, 4),
(5, 1),
(5, 2),
(5, 4),
(5, 5);


SET IDENTITY_INSERT [TaskItems] ON
INSERT INTO [TaskItems] (Id, ExternalId, Title, Description, DueAt, CreatedAt, Active, Status, Priority, ProjectId, UserId) VALUES
(1, NEWID(), 'Setup CI/CD', 'Configure continuous integration and deployment for Taskfy.', DATEADD(day, 7, GETDATE()), GETDATE(), 1, 1, 3, 1, 1),
(2, NEWID(), 'User Authentication', 'Implement OAuth2 login for users.', DATEADD(day, 10, GETDATE()), GETDATE(), 1, 1, 2, 1, 2),
(3, NEWID(), 'Task Board UI', 'Design the main task board interface.', DATEADD(day, 14, GETDATE()), GETDATE(), 1, 2, 2, 1, 3),
(4, NEWID(), 'Email Notifications', 'Send email on task assignment.', DATEADD(day, 12, GETDATE()), GETDATE(), 1, 1, 1, 1, 4),
(5, NEWID(), 'Admin Dashboard', 'Create dashboard for admins.', DATEADD(day, 20, GETDATE()), GETDATE(), 1, 2, 3, 1, 5),

-- Project 2: Mobile App Redesign
(6, NEWID(), 'Update Color Scheme', 'Apply new branding colors.', DATEADD(day, 8, GETDATE()), GETDATE(), 1, 1, 2, 2, 1),
(7, NEWID(), 'Navigation Refactor', 'Improve app navigation flow.', DATEADD(day, 11, GETDATE()), GETDATE(), 1, 1, 1, 2, 2),
(8, NEWID(), 'Push Notifications', 'Integrate push notification service.', DATEADD(day, 15, GETDATE()), GETDATE(), 1, 2, 3, 2, 3),
(9, NEWID(), 'Profile Screen', 'Redesign user profile screen.', DATEADD(day, 13, GETDATE()), GETDATE(), 1, 1, 2, 2, 4),
(10, NEWID(), 'Accessibility Audit', 'Ensure app meets accessibility standards.', DATEADD(day, 18, GETDATE()), GETDATE(), 1, 2, 2, 2, 5),

-- Project 3: API Integration
(11, NEWID(), 'Connect Payment API', 'Integrate Stripe for payments.', DATEADD(day, 9, GETDATE()), GETDATE(), 1, 1, 3, 3, 1),
(12, NEWID(), 'Sync User Data', 'Implement user data sync with CRM.', DATEADD(day, 12, GETDATE()), GETDATE(), 1, 1, 2, 3, 2),
(13, NEWID(), 'Error Handling', 'Improve API error responses.', DATEADD(day, 16, GETDATE()), GETDATE(), 1, 2, 1, 3, 3),
(14, NEWID(), 'API Rate Limiting', 'Add rate limiting to endpoints.', DATEADD(day, 14, GETDATE()), GETDATE(), 1, 1, 2, 3, 4),
(15, NEWID(), 'API Documentation', 'Publish OpenAPI docs.', DATEADD(day, 19, GETDATE()), GETDATE(), 1, 2, 2, 3, 5),

-- Project 4: Marketing Website
(16, NEWID(), 'Landing Page', 'Design new landing page.', DATEADD(day, 7, GETDATE()), GETDATE(), 1, 1, 2, 4, 1),
(17, NEWID(), 'SEO Optimization', 'Improve search engine ranking.', DATEADD(day, 10, GETDATE()), GETDATE(), 1, 1, 1, 4, 2),
(18, NEWID(), 'Blog Integration', 'Add blog section to site.', DATEADD(day, 13, GETDATE()), GETDATE(), 1, 2, 2, 4, 3),
(19, NEWID(), 'Contact Form', 'Implement contact form with validation.', DATEADD(day, 11, GETDATE()), GETDATE(), 1, 1, 2, 4, 4),
(20, NEWID(), 'Analytics Setup', 'Integrate Google Analytics.', DATEADD(day, 17, GETDATE()), GETDATE(), 1, 2, 3, 4, 5),

-- Project 5: Internal Tools
(21, NEWID(), 'Employee Directory', 'Create searchable employee directory.', DATEADD(day, 8, GETDATE()), GETDATE(), 1, 1, 2, 5, 1),
(22, NEWID(), 'Time Tracking', 'Implement time tracking for employees.', DATEADD(day, 11, GETDATE()), GETDATE(), 1, 1, 1, 5, 2),
(23, NEWID(), 'Resource Booking', 'Allow booking of meeting rooms.', DATEADD(day, 15, GETDATE()), GETDATE(), 1, 2, 2, 5, 3),
(24, NEWID(), 'Admin Permissions', 'Set up admin roles and permissions.', DATEADD(day, 13, GETDATE()), GETDATE(), 1, 1, 3, 5, 4),
(25, NEWID(), 'Reporting Module', 'Generate usage reports.', DATEADD(day, 18, GETDATE()), GETDATE(), 1, 2, 2, 5, 5),

-- Project 6: Customer Portal
(26, NEWID(), 'Login Page', 'Design customer login page.', DATEADD(day, 7, GETDATE()), GETDATE(), 1, 1, 2, 6, 1),
(27, NEWID(), 'Support Chat', 'Integrate live chat support.', DATEADD(day, 10, GETDATE()), GETDATE(), 1, 1, 1, 6, 2),
(28, NEWID(), 'Order History', 'Display past orders to customers.', DATEADD(day, 14, GETDATE()), GETDATE(), 1, 2, 2, 6, 3),
(29, NEWID(), 'Profile Management', 'Allow customers to update profiles.', DATEADD(day, 12, GETDATE()), GETDATE(), 1, 1, 2, 6, 4),
(30, NEWID(), 'Feedback Form', 'Collect customer feedback.', DATEADD(day, 17, GETDATE()), GETDATE(), 1, 2, 3, 6, 5),

-- Project 7: Analytics Dashboard
(31, NEWID(), 'KPI Widgets', 'Develop key performance indicator widgets.', DATEADD(day, 8, GETDATE()), GETDATE(), 1, 1, 2, 7, 1),
(32, NEWID(), 'Data Export', 'Enable CSV export of analytics.', DATEADD(day, 11, GETDATE()), GETDATE(), 1, 1, 1, 7, 2),
(33, NEWID(), 'User Segmentation', 'Add segmentation by user type.', DATEADD(day, 15, GETDATE()), GETDATE(), 1, 2, 2, 7, 3),
(34, NEWID(), 'Real-time Updates', 'Implement real-time data refresh.', DATEADD(day, 13, GETDATE()), GETDATE(), 1, 1, 3, 7, 4),
(35, NEWID(), 'Dashboard Sharing', 'Allow users to share dashboards.', DATEADD(day, 18, GETDATE()), GETDATE(), 1, 2, 2, 7, 5),

-- Project 8: Onboarding Flow
(36, NEWID(), 'Welcome Email', 'Send welcome email to new users.', DATEADD(day, 7, GETDATE()), GETDATE(), 1, 1, 2, 8, 1),
(37, NEWID(), 'Tutorial Screens', 'Create onboarding tutorial screens.', DATEADD(day, 10, GETDATE()), GETDATE(), 1, 1, 1, 8, 2),
(38, NEWID(), 'Progress Tracking', 'Track onboarding progress.', DATEADD(day, 14, GETDATE()), GETDATE(), 1, 2, 2, 8, 3),
(39, NEWID(), 'Referral Program', 'Implement referral incentives.', DATEADD(day, 12, GETDATE()), GETDATE(), 1, 1, 2, 8, 4),
(40, NEWID(), 'Feedback Survey', 'Collect feedback after onboarding.', DATEADD(day, 17, GETDATE()), GETDATE(), 1, 2, 3, 8, 5),

-- Project 9: Notification Service
(41, NEWID(), 'Email Service', 'Set up transactional email service.', DATEADD(day, 8, GETDATE()), GETDATE(), 1, 1, 2, 9, 1),
(42, NEWID(), 'SMS Integration', 'Integrate SMS notifications.', DATEADD(day, 11, GETDATE()), GETDATE(), 1, 1, 1, 9, 2),
(43, NEWID(), 'Push Notification API', 'Develop push notification endpoints.', DATEADD(day, 15, GETDATE()), GETDATE(), 1, 2, 2, 9, 3),
(44, NEWID(), 'Notification Preferences', 'Allow users to set notification preferences.', DATEADD(day, 13, GETDATE()), GETDATE(), 1, 1, 3, 9, 4),
(45, NEWID(), 'Delivery Reports', 'Generate notification delivery reports.', DATEADD(day, 18, GETDATE()), GETDATE(), 1, 2, 2, 9, 5),

-- Project 10: Billing System
(46, NEWID(), 'Invoice Generation', 'Automate invoice creation.', DATEADD(day, 7, GETDATE()), GETDATE(), 1, 1, 2, 10, 1),
(47, NEWID(), 'Payment Gateway', 'Integrate payment gateway.', DATEADD(day, 10, GETDATE()), GETDATE(), 1, 1, 1, 10, 2),
(48, NEWID(), 'Subscription Plans', 'Add support for multiple plans.', DATEADD(day, 14, GETDATE()), GETDATE(), 1, 2, 2, 10, 3),
(49, NEWID(), 'Tax Calculation', 'Implement tax calculation logic.', DATEADD(day, 12, GETDATE()), GETDATE(), 1, 1, 2, 10, 4),
(50, NEWID(), 'Billing History', 'Show billing history to users.', DATEADD(day, 17, GETDATE()), GETDATE(), 1, 2, 3, 10, 5);

SET IDENTITY_INSERT [TaskItems] OFF
