<h1 style="text-align:center !important;"> SNHU Capstone Project Page </h1>
<h2 style="text-align:center !important;"> By Alan Davis </h2>
<h3 style="text-align:center !important;"> August 14th, 2021 </h3>

<h4>Intro / Professional Self-Assessment</h4>

<p>
    I have greatly enjoyed my career as a student in the SNHU Computer Science program. There was a lot to learn about theory, technology, and the application of programming principles in software development. Although my time in the program is coming to an end, and I will be happily graduating with my degree in Computer Science, I am aware that the professional field of technology is an ever-evolving space, and I will have to be proactive about learning new developments in the industry to keep my skills and coding practices sharp. 
</p>

<p>
  	There was a lot that I have learned about the underlying Computer Science fundamentals in this program. Specifically, the role that Data Structures and Algorithms play in software development. I believe that a lot of the fundamentals are being abstracted away with new technologies that make a developer’s life easy, however it is important to be aware of the fact that all of these new technologies are relying on the same fundamentals that got us to where we are here today with respect to all things Computer Science. So, having had the experience of learning about the fundamentals, I have attained a great appreciation of their importance and the role they play in the grand scheme of things. 
</p>

<p>
  	With regards to the capstone course, I am appreciative of the fact that I was able to bring all my knowledge together and demonstrate it in the form of updating an existing application that I have worked on in the past. It was a great recap of the many things I have learned thus far, and their application to an actual scenario, which I will be further updating in the future. 
</p>

<p>
  	I cannot wait to showcase my skills to my future employers and start my journey towards a career as a technology professional. 
</p>
   
 <h4> Milestone One - Code Review </h4>
 
 <iframe width="560" height="315" src="https://www.youtube.com/embed/5gHMVmOznog" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>
 
 
 <h4> Milestone Two - Enhancement One: Software Design/Engineering </h4>
 
 
<p>
  	The artifact I am including for this assignment is a code file that serves as the code-behind for the Controller Methods of the ASP.Net MVC web application project that is written in C#. This file contains methods for the various controller actions that are responsible for building the Model objects and returning them along with the Views to the user as a response to their HTTP request. This controller is responsible for the vast majority of the pages that the user will be interacting with, it was created as each individual View of the web application was created and required an associated controller method. 
</p>

<p>
  	This artifact was included because it showcases the majority of the logic that is used by the application, and it is a good example that showcases my ability to utilize various programming techniques to manipulate the behavior and output of the application based on user input. I have made several changes to the artifact that I have identified as beneficial to enhancing the code’s readability and maintainability, exception handling, as well as security of the application. 
</p>
<p>
  	Throughout the code, I added comments that enable both myself and future developers understand the functionality a code segment is attempting to achieve. I’ll include snippets from a diff view in my IDE to demonstrate the changes I’ve made below: 
</p>
<p>
  	In addition to adding comments that help the reader trace the code, I have also decoupled the generation of the model object from the View method that is being called by the controller method. This allows for better readability, because the reader will know what type of object is being generated for the View model.
</p>
<p>
  	Further, I made changes to my switch statement. Where before I used to return the View that had a LINQ expression generating the model object by querying the database through Entity Framework. Now, my switch statement no longer returns the view after each case, but assigns the value of the defined model object in the beginning of the controller method, and the object is then separately passed to the View method that is called at the return statement of the controller. This mitigates user error and code duplication, by reducing the number of times that a returned View method has to be defined after each ‘case’ of a switch statement. 
</p>
<p>
  	Another change that I have introduced to the artifact were more checks to determine whether the supplied model object is valid, that is, if it conforms to the model data definition, before the code continues any further. 
</p>
<p>
  	In the above snippet, you can see that I added logic to check whether the Model State of the supplied model object is valid. If it is not, then the user is returned to the original view that is making the request. Further, as I discussed in my code review, I now strive to follow better naming conventions, and therefore renamed the one of the objects from ‘item’ to ‘collectionObject’, which follows camel-case formatting, and enhances readability. Comments were also added here to make my intent clear, specifically as to why a separate object was declared of the same type as the model that was already passed into the method. In this case, it was to circumvent a vulnerability in this web-application known as ‘overposting’. 
</p>
<p>
  	Yet another change I made to the artifact involves additional checks around file operations. Specifically, whether the file already exists within a directory, before my application attempts to create another file with the same name inside a folder – thereby causing an exception. 
</p>
<p>
  	Here you can see that I added an if clause, which checks whether a file with the specified path already exists. If it does not, then the file is created within the system. Otherwise, an exception is thrown and is caught and processed by the ‘catch’ block of the code segment further down in the method. I also added comments to make this apparent. 
</p>
<p>
  	I believe that I have satisfied the course objectives defined in the earlier module. Part of writing good software is writing clear and concise code, which I think I am getting more proficient at, with better usage of comments and other programming practices that help both myself, and other developers who may look at this application down the line, quickly and seamlessly trace the code and understand my intent when I was originally writing this logic. 
</p>
<p>
  	While making changes to this artifact, I really enjoyed reflecting on my coding practices before my experience in the relevant Computer Science courses, and how my coding style and perspective has changed since then. What this assignment is, essentially, is a refactoring exercise. And one challenge I have encountered is the importance of being judicious of my time and effort invested when reviewing code. Because, since I have picked up many new coding techniques over time, as I was reviewing my code, many times I was tempted to overhaul the method and re-write it differently. However, a refactor does not mean a complete re-write of the application, performing this exercise made me appreciate the challenge of identifying potentially problematic code and having to trace through blocks of ambiguous and undocumented code, because I had forgotten what the code was trying to accomplish. Subsequently adding clear comments where applicable will serve me and possibly other developers well into the future.  
</p>

<h4>Milestone Three – Algorithms and Data Structures</h4>
<p>
  	
</p>

