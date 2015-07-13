//var stars = [
//    { name: "A", x: 0, y: 0, z: 0, r: 0, g: 1, b: 0 }
//];

var d = document.createElement("div");
document.body.appendChild(d);
d.style.width = "400px";
d.style.height = "400px";
d.style["background-color"] = "black";
d.id = "container";

var s = document.createElement("script");
s.type = "text/javascript";
s.src = "//cdnjs.cloudflare.com/ajax/libs/three.js/r71/three.min.js";
document.head.appendChild(s);

s.onload = function () {

	var camera, scene, renderer;
	var mesh;

	init();
	animate();

	function init() {

		container = document.getElementById('container');

		//

		camera = new THREE.PerspectiveCamera(27, window.innerWidth / window.innerHeight, 5, 3500);
		camera.position.z = 2750;

		scene = new THREE.Scene();
		scene.fog = new THREE.Fog(0x050505, 2000, 3500);

		//

		var particles = stars.length;

		var geometry = new THREE.BufferGeometry();

		var positions = new Float32Array(particles * 3);
		var colors = new Float32Array(particles * 3);

		var color = new THREE.Color();

		var n = 1000, n2 = n / 2; // particles spread in the cube

		for (var i = 0; i < positions.length; i += 3) {

			var star = stars[i / 3];

			// positions
			positions[i] = star.x;
			positions[i + 1] = star.y;
			positions[i + 2] = star.z;

			// colors
			var vx = (star.r);
			var vy = (star.g);
			var vz = (star.b);
			color.setRGB(vx, vy, vz);

			colors[i] = color.r;
			colors[i + 1] = color.g;
			colors[i + 2] = color.b;
		}

		geometry.addAttribute('position', new THREE.BufferAttribute(positions, 3));
		geometry.addAttribute('color', new THREE.BufferAttribute(colors, 3));

		geometry.computeBoundingSphere();

		//

		var material = new THREE.PointCloudMaterial({ size: 15, vertexColors: THREE.VertexColors });

		particleSystem = new THREE.PointCloud(geometry, material);
		scene.add(particleSystem);

		//

		renderer = new THREE.WebGLRenderer({ antialias: false });
		renderer.setClearColor(scene.fog.color);
		renderer.setPixelRatio(window.devicePixelRatio);
		renderer.setSize(window.innerWidth, window.innerHeight);

		container.appendChild(renderer.domElement);

		//


		window.addEventListener('resize', onWindowResize, false);

	}

	function onWindowResize() {

		camera.aspect = window.innerWidth / window.innerHeight;
		camera.updateProjectionMatrix();

		renderer.setSize(window.innerWidth, window.innerHeight);

	}

	//

	function animate() {

		requestAnimationFrame(animate);

		render();

	}

	function render() {

		var time = Date.now() * 0.001;

		particleSystem.rotation.x = time * 0.25;
		particleSystem.rotation.y = time * 0.5;

		renderer.render(scene, camera);

	}
}
