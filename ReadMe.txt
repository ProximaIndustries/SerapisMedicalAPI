This is the new readme file created on 17 October 2019

Key Points 
	1) Infrastructure layout
	2) Code Style & TDD Approach
	3) Unit Tests
	4) CI & using Circle CI & Github



1) SerapisMedicalAPI => This maps the layers that hold WEB/UI or presenter concerns(Basically the contrllers since we dont have a UI).
						 This is done due to the Dependecy Rule of SOLID principles.
	
   SerapisMedicalAPI.Core => This Maps to the Use Case/Business logic we implement.(Domain objects,business rules, databases etc). Interfaces are used to represent those depedencies and get injected.
	
   SerapisMedicalAPI.Infrastructure => Holds he database and gateway concerns. We define the database structure,access, services and caching

