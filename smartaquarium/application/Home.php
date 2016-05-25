<?php
defined('BASEPATH') OR exit('No direct script access allowed');

class Home extends CI_Controller {
	
	public function __construct()
{
	parent::__construct();
	#load_library dan helper yang dibutuhkan
	$this->load->library(array('table','form_validation'));
	$this->load->helper(array('form','url'));
	$this->load->model('home_m','',TRUE);
}

	public function index()
	{
		$jumlah_data=$this->home_m->jumlah_data();
		$this->load->library('pagination');
		$this->load->helper('url');
		$data['query']=$this->home_m->index();
		$this->load->view('/web/index.php', $data);
		$this->load->model('home_m');
		$config['total_rows']=$jumlah_data;
		$config['per_page']=10;

	}
	
	public function _set_rules(){

	$this->form_validation->set_rules('id_air','Id','required\trim');
	$this->form_validation->set_rules('status','Status','required\trim');
	$this->form_validation->set_rules('tanggal','Tanggal','required\trim');
	$this->form_validation->set_rules('pass','Password','required\trim');
}	
}