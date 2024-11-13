<script>
  // You can define the links to your demo and register pages here
  const demoLink = "/demo";
  const registerLink = "/register";
</script>

<div class="hero">
  <div class="overlay"></div>
  <!-- Dark overlay over the background -->
  <h1>Welcome to Our Platform</h1>
  <div class="buttons">
    <a href={demoLink} class="button">Check Out the Demo</a>
    <a href={registerLink} class="button">Register Now</a>
  </div>
</div>

<style>
  /* Basic styling for the hero section */
  .hero {
    height: 100vh;
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    text-align: center;
    background-image: url("https://img.freepik.com/free-vector/black-background-with-blue-purple-wave-design_483537-4450.jpg");
    background-size: cover;
    background-position: center;
    background-repeat: no-repeat;
    position: relative;
  }

  .overlay {
    position: absolute;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background: rgba(
      0,
      0,
      0,
      0.5
    ); /* Semi-transparent overlay for better readability */
  }

  h1 {
    color: white;
    font-size: 3rem;
    z-index: 2;
  }

  .buttons {
    margin-top: 20px;
    z-index: 2;
  }

  .button {
    padding: 15px 30px;
    margin: 10px;
    background-color: #00adb5;
    color: white;
    font-size: 1.2rem;
    border: none;
    border-radius: 5px;
    cursor: pointer;
    text-decoration: none;
    transition: background-color 0.3s;
  }

  .button:hover {
    background-color: #007a7f;
  }
</style>
